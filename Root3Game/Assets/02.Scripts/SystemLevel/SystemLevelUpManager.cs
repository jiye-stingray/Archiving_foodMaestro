using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemLevelUpManager : Singleton<SystemLevelUpManager>
{
    [SerializeField] List<SystemLevelUpData> _stageSystemLevelUpDataList;

    public List<SystemData> ReturnSystemDataListInStage(int index)
    {
        return _stageSystemLevelUpDataList[index]._datasList.ToList();
    }
}
