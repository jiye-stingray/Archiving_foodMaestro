using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SystemLevelUpData", menuName = "Data/SystemLevelUp")]
public class SystemLevelUpData : ScriptableObject
{

    // 현재 스테이지에 필요한 데이터 갯수
    // worker 4개 
    // chair  4개 
    // 각 food Data 마다 금액 증가 & 속도 감소 필요
    // Time 은 1.5 
    // 3 + 4 + 3(음식 갯수) * 2 = 13 개

    public List<SystemData> _datasList;
}
