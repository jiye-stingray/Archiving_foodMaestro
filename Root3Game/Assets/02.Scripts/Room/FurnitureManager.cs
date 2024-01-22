using Model;
using System.Collections.Generic;
using UnityEngine;



public class FurnitureManager : Singleton<FurnitureManager>
{
    [Tooltip("모든 가구들")]
    public List<FurnitureData> _furnitureDataList = new List<FurnitureData>();
    [Tooltip("모든 가구 오브젝트 프리팹")]
    public List<GameObject> _furniturePrefab = new List<GameObject>();


    [Tooltip("인벤토리의 가구들")]
    public Dictionary<int, int> _inventoryFurnitureDic = new Dictionary<int, int>();

    public int _selectInventoryFurnitureIndex = -1;

    [SerializeField] Transform _unit;
    public ScrollerController _scrollerController;

    SaveManager _saveManager => SaveManager.Instance;
    GridManager _gridManager => GridManager.Instance;

    public void AddFurnitureInInventory(int index)
    {
        _inventoryFurnitureDic[index]++;
        _scrollerController._scroller.RefreshActiveCellViews();
    }

    public void RemoveFurnitureInInventory(int index)
    {
        _selectInventoryFurnitureIndex = -1;
        _inventoryFurnitureDic[index]--;
        _scrollerController._scroller.RefreshActiveCellViews();
    }

    /// <summary>
    /// 인벤토리에서 설치할 가구 오브젝트를 생성 후 반환
    /// </summary>
    /// <param name="data"></param>
    public void SetFurniture(FurnitureData data)
    {
        Furniture f = Instantiate(_furniturePrefab[data._index], transform.position, Quaternion.identity).GetComponent<Furniture>();
        f.transform.SetParent(_unit);
        _gridManager.SetStartFurniture(f);
    }

    #region Save

    /// <summary>
    /// Load한 가구 배치 불러오기
    /// </summary>
    public void CreateLoadFurniture()
    {

        List<Furniture> list = _gridManager._installationFurnitureList;         // 기존 데이터 리스트
        List<Furniture> updateList = new List<Furniture>();             // 새로 추가될 설치 리스트

        for (int i = 0; i < list.Count; i++)
        {
            Furniture f = Instantiate(_furniturePrefab[list[i]._furnitureData._index], transform.position, Quaternion.identity).GetComponent<Furniture>();
            f.transform.SetParent(_unit);

            f.Move(_gridManager.tiles.tiles[list[i].x, list[i].y]);
            f.Rotate(list[i].direction);
            _gridManager.OnPlaceFurniture(f);

            updateList.Add(f);
        }

        _gridManager._installationFurnitureList = updateList;

        SaveManager.Instance.SaveFurniture(_gridManager._installationFurnitureList);
    }

    #endregion
}
