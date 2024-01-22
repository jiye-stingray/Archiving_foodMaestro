using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSystemLevelUpDataManager : Singleton<CurrentSystemLevelUpDataManager>
{

    public List<SystemData> _currentSystemDataList = new List<SystemData>();


    public override void Awake()
    {
        base.Awake();
        SaveManager.Instance.LoadSystemLevelUpUI(); // Data List 받아오기

    }

    public void RemoveSystemDataList(SystemData data)
    {
        _currentSystemDataList.Remove(data);
        SaveManager.Instance.SaveSystemLevelUpUI(_currentSystemDataList);
    }
}
