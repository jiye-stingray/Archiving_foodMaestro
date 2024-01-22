using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageFoodData
{
    public FoodData[] _foodDatas;
    public float[] _variableGoldValue;          // 필요한 금액 
    public float[] _varialbleTimeValue;         // 소모 시간 
}


public class FoodManager : Singleton<FoodManager>
{
    [SerializeField] private List<StageFoodData> _stageFoodDatasList = new List<StageFoodData>();

    private StageFoodData _currentStageFoodDatas;
    public StageFoodData CurrentStageFoods {  get { return _currentStageFoodDatas; } }

    public List<int> _canFoodIDList = new List<int>();

    public override void Awake()
    {
        base.Awake();
        SetFoodData(0);     // 추후 GameManager의 stage 인덱스 할당

        OpenNewFood(0);     // 임시  
        OpenNewFood(1);     // 임시  
        OpenNewFood(2);     // 임시  

    }

    private void Start()
    {
        
    }

    private void SetFoodData(int stage)
    {
        _currentStageFoodDatas = _stageFoodDatasList[stage];
        _currentStageFoodDatas._variableGoldValue = new float[_currentStageFoodDatas._foodDatas.Length];
        _currentStageFoodDatas._varialbleTimeValue = new float[_currentStageFoodDatas._foodDatas.Length];
    }

    /// <summary>
    /// 요리 가능한 요리 추가 
    /// 아마 kitchens 에서 호출할 예정
    /// </summary>
    /// <param name="index"></param>
    public void OpenNewFood(int index)
    {
        _canFoodIDList.Add(index);
    }
}
