using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model;
using System.Collections;


public class GridManager : Singleton<GridManager>
{

    public Tiles tiles;
    private Sorter sorter;


    private bool dragging = false;

    public Transform interactBtnGroup;
    public Button placeButton;
    public Button rotateButton;
    public Button undoButton;
    public Toggle mode;
    public SpriteRenderer grids;
    public GameObject checkObj;

    [Header("Furniture")]
    [HideInInspector] public Furniture SelectedFurniture;
    public List<Furniture> _installationFurnitureList = new List<Furniture>();         // 방에 설치된 가구
    [Tooltip("가구 인벤토리")]
    [SerializeField] public GameObject _furnitureInventoryPanel;

    FurnitureManager _furnitureManager => FurnitureManager.Instance;
    SaveManager _saveManager => SaveManager.Instance;

    public GameObject _clcikedSelectButton;

    public override void Awake()
    {
        base.Awake();
        sorter = GameObject.Find("Unit").GetComponent<Sorter>();
        tiles = GameObject.Find("Tiles").GetComponent<Tiles>();
    }

    void Start()
    {
        grids.enabled = false;

        placeButton.onClick.AddListener(() =>
        {

            if (!_installationFurnitureList.Contains(SelectedFurniture))            // 배치된 가구 리스트 추가
            {
                _installationFurnitureList.Add(SelectedFurniture);
                // 저장 필요
            }
            _saveManager.SaveFurniture(_installationFurnitureList);
            _saveManager.SaveFurniture(_furnitureManager._inventoryFurnitureDic);


            foreach (Furniture item in _installationFurnitureList)                  // 배치된 가구 색 다시 원상복구
            {
                item._selectedDirectionItemSprite.color = Color.white;
            }

            OnPlaceFurniture(SelectedFurniture);
            sorter.SortAll();
            interactBtnGroup.gameObject.SetActive(false);
        });
        rotateButton.onClick.AddListener(() =>
        {
            List<Tile> area;
            RotateItem(out area);
        });
        undoButton.onClick.AddListener(() =>
        {

            //OnUndo(SelectedFurniture);

            _installationFurnitureList.Remove(SelectedFurniture);
            FurnitureManager.Instance.AddFurnitureInInventory(SelectedFurniture._furnitureData._index);

            // 저장 처리 필요 
            _saveManager.SaveFurniture(_installationFurnitureList);
            _saveManager.SaveFurniture(_furnitureManager._inventoryFurnitureDic);

            StartCoroutine(DestroyAfterSortDelayCor());

            interactBtnGroup.gameObject.SetActive(false);
        });
        mode.onValueChanged.AddListener(value =>
        {
            grids.enabled = value;
            checkObj.SetActive(value);
            FurnitureManager.Instance._selectInventoryFurnitureIndex = -1;
            _furnitureInventoryPanel.SetActive(value);
        });
    }

    /// <summary>
    /// Destroy 후 frame waiting.
    /// Destroy는 다음 frame 에 삭제 처리 됨
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyAfterSortDelayCor()
    {
        Destroy(SelectedFurniture.gameObject);
        SelectedFurniture = null;

        yield return new WaitForEndOfFrame();

        foreach (Furniture item in _installationFurnitureList)                  // 배치된 가구 색 다시 원상복구
        {
            item._selectedDirectionItemSprite.color = Color.white;
        }

        sorter.SortAll();
    }

    void Update()
    {

        if (!mode.isOn)
            return;
        mode.interactable = SelectedFurniture == null;

        if (Input.GetMouseButtonDown(0))
            OnBeginDrag(isHold => dragging = isHold);

        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            OnEndDrag();
        }

        if (dragging)
        {
            OnDrag();
            sorter.SortAll();
        }

    }

    private void OnBeginDrag(Action<bool> isHold)
    {
        if (SelectedFurniture == null)
        {
            var furniture = OnSelect(child => child.transform.parent.GetComponent<Furniture>() != null);
            if (furniture != null)
            {
                SelectedFurniture = furniture.transform.parent.GetComponent<Furniture>();
                SelectedFurniture.Unplaced();

                ResetInventoryButton();
            }
            isHold(furniture != null);
        }
        else
        {
            var furniture = OnSelect(child => child.transform.parent.GetComponent<Furniture>() != null && child.transform.parent.GetComponent<Furniture>() == SelectedFurniture);

            ResetInventoryButton();

            isHold(furniture != null);

        }

    }

    private void OnDrag()
    {
        if (SelectedFurniture == null)
            return;

        foreach (Furniture item in _installationFurnitureList)              // 선택되지 않은 가구들은 투명화
        {
            if (item == SelectedFurniture) continue;
            item._selectedDirectionItemSprite.color = new Color(1, 1, 1, 0.6f);
        }

        var tile = OnSelect(obj => obj.GetComponent<Tile>() != null);
        if (tile != null)
        {
            interactBtnGroup.gameObject.SetActive(false);
            SelectedFurniture.Move(tile.GetComponent<Tile>());

            List<Tile> area;
            OnInvalid(SelectedFurniture, out area);
        }
    }

    private void OnEndDrag()
    {
        if (SelectedFurniture == null)
            return;


        var centerPoint = Camera.main.WorldToScreenPoint(SelectedFurniture.transform.position);
        interactBtnGroup.position = centerPoint;
        interactBtnGroup.gameObject.SetActive(true);

        List<Tile> area;
        placeButton.interactable = !(OnInvalid(SelectedFurniture, out area));
        undoButton.interactable = SelectedFurniture.previous != null;
    }

    private void RotateItem(out List<Tile> area)
    {
        area = new List<Tile>();
        if (SelectedFurniture != null)
        {
            SelectedFurniture.Rotate();
            placeButton.interactable = !(OnInvalid(SelectedFurniture, out area));
        }
    }

    private GameObject OnSelect(Predicate<GameObject> condition)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, Mathf.Infinity);
        foreach (var hit in hits)
            if (condition(hit.transform.gameObject))
                return hit.transform.gameObject;
        return null;
    }

    public void OnPlaceFurniture(Furniture furniture)
    {
        if (furniture == null)
            return;

        List<Tile> area;
        if (!OnInvalid(furniture, out area))
        {
            furniture.Place(area);
            furniture.SetColor(Color.white);
            SelectedFurniture = null;

            sorter.SortAll();
        }
    }

    private bool OnInvalid(Furniture furniture, out List<Tile> area)
    {

        area = new List<Tile>();

        if (furniture.origin == null)            // 가구 배치 시작 예외처리
        {
            // 현재 위치의 tile 할당 해줘야 함 
            Tile tile = tiles.tiles[0, 0];

            furniture.Move(tile);

        }

        for (int i = 0; i < furniture.width; i++)
        {
            for (int j = 0; j < furniture.length; j++)
            {
                var tile = tiles.GetTileByCoordinate(furniture.origin.x + j, furniture.origin.y + i);

                if (tile == null || tile.isBlock)
                {
                    furniture.SetColor(Color.red);
                    return true;
                }

                area.Add(tile);
            }
        }

        furniture.SetColor(Color.green);
        return false;
    }

    private void OnUndo(Furniture furniture)
    {

        if (furniture.previous == null)
            return;

        furniture.Move(furniture.previous.tile);
        furniture.Rotate(furniture.previous.direction);
        OnPlaceFurniture(furniture);
    }

    #region Custom

    /// <summary>
    /// 함수를 인벤토리에서 처음 배치
    /// </summary>
    /// <param name="f"></param>
    public void SetStartFurniture(Furniture f)
    {
        SelectedFurniture = f;
        foreach (Furniture item in _installationFurnitureList)              // 선택되지 않은 가구들은 투명화
        {
            if (item == SelectedFurniture) continue;
            item._selectedDirectionItemSprite.color = new Color(1, 1, 1, 0.6f);
        }

        OnBeginDrag(isHold => dragging = isHold);
    }

    private void ResetInventoryButton()
    {
        // 인벤토리 가구 선택 버튼 초기화
        _furnitureManager._selectInventoryFurnitureIndex = -1;
        _clcikedSelectButton?.SetActive(false);
        _clcikedSelectButton = null;
    }

    #endregion
}
