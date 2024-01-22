using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureButton : EnhancedScrollerCellView
{
    public FurnitureData _furnitureData;            // 추후 동적 할당
    private Image _img;
    [SerializeField] private Button _selectBtn;


    GridManager _gridManager => GridManager.Instance;
    FurnitureManager _furnitureManager => FurnitureManager.Instance;

    [Header("Count")]
    public TMP_Text _countText;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    private void Start()
    {
        _selectBtn.onClick.AddListener(() => SelectBtnClick());
        _selectBtn.gameObject.SetActive(false);

    }

    public void Init(FurnitureData data)
    {
        _furnitureData = data;
        _img.sprite = _furnitureData._sprite;

        RefreshCellView();

    }

    public void FurnitureBtnClick()
    {
        if (_furnitureManager._inventoryFurnitureDic[_furnitureData._index] <= 0) return;

        if (_gridManager.SelectedFurniture == null)             // 배치 중이 아닐때
        {
            FurnitureManager.Instance._selectInventoryFurnitureIndex = _furnitureData._index;
            _gridManager._clcikedSelectButton = _selectBtn.gameObject;
            _furnitureManager._scrollerController._scroller.RefreshActiveCellViews();
        }

    }

    public void SelectBtnClick()
    {
        _gridManager._clcikedSelectButton = null;

        FurnitureManager.Instance.SetFurniture(_furnitureData);
        FurnitureManager.Instance.RemoveFurnitureInInventory(_furnitureData._index);
        _selectBtn.gameObject.SetActive(false);


    }

    public override void RefreshCellView()
    {
        base.RefreshCellView();
        if (FurnitureManager.Instance._inventoryFurnitureDic.Count != 0)
            _countText.text = FurnitureManager.Instance._inventoryFurnitureDic[_furnitureData._index].ToString();
        _selectBtn.gameObject.SetActive(_furnitureManager._selectInventoryFurnitureIndex == _furnitureData._index);
    }

}
