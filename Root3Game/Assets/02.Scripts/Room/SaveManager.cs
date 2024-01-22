using Model;
using System.Collections.Generic;

public class SaveManager : Singleton<SaveManager>
{

    GameManager _gameManager => GameManager.Instance;

    #region Stage 



    public void SaveStage()
    {
        ES3.Save("stage", _gameManager._stage);
    }

    public void LoadStage()
    {
        if(ES3.KeyExists("stage"))
        {
            _gameManager._stage = ES3.Load<int>("stage");
        }
    }

    #endregion

    #region Furniture
    GridManager _gridManager => GridManager.Instance;
    FurnitureManager _furnitureManager => FurnitureManager.Instance;

    public void SaveFurniture<T>(T data)
    {
        if (typeof(T) == typeof(Dictionary<int, int>))
        {
            // 인벤토리 저장
            ES3.Save("inventoryFurnitureData", _furnitureManager._inventoryFurnitureDic);
        }
        else
        {
            // 방에 설치되는 가구를 저장하자
            ES3.Save("FurnitureObj", _gridManager._installationFurnitureList);
        }
        _furnitureManager._scrollerController.ReloadData();
    }


    /// <summary>
    /// 인벤토리 데이터 로드하기
    /// </summary>
    private void LoadInvenFurnitureData()
    {
        if (ES3.KeyExists("inventoryFurnitureData"))
        {
            _furnitureManager._inventoryFurnitureDic = ES3.Load<Dictionary<int, int>>("inventoryFurnitureData");

        }
        else
        {
            for (int i = 0; i < _furnitureManager._furnitureDataList.Count; i++)
            {
                _furnitureManager._inventoryFurnitureDic.Add(_furnitureManager._furnitureDataList[i]._index, 0);
            }

            _furnitureManager.AddFurnitureInInventory(0);
            _furnitureManager.AddFurnitureInInventory(0);
            _furnitureManager.AddFurnitureInInventory(1);
            _furnitureManager.AddFurnitureInInventory(2);

        }

        _gridManager._furnitureInventoryPanel.SetActive(false);

    }

    /// <summary>
    /// 배치된 가구 데이터 로딩하기
    /// </summary>
    private void LoadFurniture()
    {

        if (ES3.KeyExists("FurnitureObj"))
        {
            // 데이터 로드하기
            _gridManager._installationFurnitureList = ES3.Load<List<Furniture>>("FurnitureObj");

            // 가구 배치하기
            _furnitureManager.CreateLoadFurniture();

        }
    }

    public void LoadFurnitureData()
    {
        LoadInvenFurnitureData();
        LoadFurniture();
    }

    #endregion

    #region Chair


    ChairManager _chairManager => ChairManager.Instance;

    public void LoadChair()
    {
        if (ES3.KeyExists("ChairCount_" + _gameManager._stage.ToString()))
        {
            _chairManager._chairCountSave = ES3.Load<int>("ChairCount_" + _gameManager._stage.ToString());

            for (int i = 0; i < _chairManager._chairCountSave; i++)
            {
                _chairManager.LoadAddNewChair();
            }
        }
        else
        {
            _chairManager._chairCountSave = 0;
            _chairManager.AddNewChair();

        }
    }

    public void SaveChair(int index)
    {
        ES3.Save("ChairCount_" + _gameManager._stage.ToString(), index);
    }

    #endregion

    #region Kitchen

    KitchenManager _kitchenManager => KitchenManager.Instance;

    public void LoadKitchen(int stage, int foodIndex)
    {
        string kitchenS = "kitchenData" + "_" + stage.ToString() + "_" + foodIndex.ToString();
        string kitchenPanelS = "kitchenPanelData" + "_" + stage.ToString() + "_" + foodIndex.ToString();

        Kitchens kitchens = _kitchenManager._kitchensList[foodIndex];
        if (ES3.KeyExists(kitchenS))
        {
            Kitchens kitchenData = ES3.Load<Kitchens>(kitchenS);

            kitchens.Level = kitchenData.Level;
            kitchens.LoadKitchenData();
        }

        if(ES3.KeyExists(kitchenPanelS))
        {
            KitchenLevelPanel kitchenLevelUpPanelData = ES3.Load<KitchenLevelPanel>(kitchenPanelS);
            
            kitchens._panel._clickCnt = kitchenLevelUpPanelData._clickCnt;
            kitchens._panel.LoadData();
        }

    }

    /// <summary>
    /// 저장
    /// </summary>
    public void SaveKitchen(string path, Kitchens kitchens)
    {
        ES3.Save<Kitchens>(path, kitchens);

    }

    #endregion

    #region Gold

    public void LoadGold()
    {
        if(ES3.KeyExists("gold"))
        {
            GoldManager.Instance.LoadSetGold(ES3.Load<float>("gold"));
        }
    }
    #endregion

    #region Worker
    public void LoadWorker()
    {
        if(ES3.KeyExists("workerCount_" + _gameManager._stage.ToString()))
        {
            int cnt = ES3.Load<int>("workerCount_" + _gameManager._stage.ToString());

            for (int i = 0; i < cnt; i++)
            {
                WorkerManager.Instance.CreateWorker();
            }
        }
    }

    public void SaveWorker(int cnt)
    {
        ES3.Save("workerCount_" + _gameManager._stage.ToString(), cnt);

    }
    #endregion

    #region SystemLevelUpPopup

    CurrentSystemLevelUpDataManager _currentSystemLevelUpDataManager => CurrentSystemLevelUpDataManager.Instance;

    public void LoadSystemLevelUpUI()
    {
        if(ES3.KeyExists("systemLevelUpUI_" + _gameManager._stage.ToString()))
        {
            _currentSystemLevelUpDataManager._currentSystemDataList = ES3.Load<List<SystemData>>("systemLevelUpUI_" + _gameManager._stage.ToString());
        }
        else
        {
            _currentSystemLevelUpDataManager._currentSystemDataList = SystemLevelUpManager.Instance.ReturnSystemDataListInStage(GameManager.Instance._stage - 1);
            
        }

    }

    public void SaveSystemLevelUpUI(List<SystemData> list)
    {
        ES3.Save("systemLevelUpUI_" + _gameManager._stage.ToString(), list);
    }
    #endregion
}
