using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum SystemType
{
    Worker,
    Chair,
    Cost,
    Time
}

[System.Serializable]
public class SystemData
{
    public SystemType _systemType;
    public FoodData _foodData;
    public float _addCostValue;
    public float _addTimeValue;
    public float _levelUpNeedGold;
}

public class SystemLevelUpContent : MonoBehaviour
{
    private SystemData _systemData;

    [SerializeField] Sprite _chairSprite;

    [SerializeField] Image _iconImg;
    [SerializeField] TMP_Text _typeText;
    [SerializeField] TMP_Text _goldText;
    [SerializeField] Image _btnImg;

    public delegate void SystemLevelDel();
    SystemLevelDel _systemLevelUpDel;

    private SystemType _systemType;
    private float _levelUpNeedGold;


    SystemLevelUpScrollView _scrollView;

    private void Awake()
    {
        _scrollView = GetComponentInParent<SystemLevelUpScrollView>();
    }


    public void Init(SystemData systemData)
    {
        _systemData = systemData;

        _systemType = _systemData._systemType;
        _levelUpNeedGold = _systemData._levelUpNeedGold;

        switch (_systemType)
        {
            case SystemType.Worker:
                _typeText.text = "직원 추가";
                break;
            case SystemType.Chair:
                _typeText.text = "의자 추가";
                _iconImg.sprite = _chairSprite;
                break;
            case SystemType.Cost:
                _typeText.text = "요리 가격 증가";
                _iconImg.sprite = _systemData._foodData._sprite;
                break;
            case SystemType.Time:
                _typeText.text = "제작 시간 감소";
                _iconImg.sprite = _systemData._foodData._sprite;

                break;
            default:
                break;
        }

        _goldText.text = _levelUpNeedGold.ToString();

        InitBtnDelEvent();
    }

    private void InitBtnDelEvent()
    {
        switch (_systemType)
        {
            case SystemType.Worker:
                _systemLevelUpDel += Worker;
                break;
            case SystemType.Chair:
                _systemLevelUpDel += Chair;
                break;
            case SystemType.Cost:
                _systemLevelUpDel += Cost;
                break;
            case SystemType.Time:
                _systemLevelUpDel += Time;
                break;
            default:
                break;
        }
        _systemLevelUpDel += Destroy;
    }

    private void Worker()
    {
        WorkerManager.Instance.CreateWorker();
    }

    private void Chair()
    {
        ChairManager.Instance.AddNewChair();
    }

    private void Cost()
    {
        FoodManager.Instance.CurrentStageFoods._variableGoldValue[_systemData._foodData._indexID] = _systemData._addCostValue;
    }

    private void Time()
    {
        FoodManager.Instance.CurrentStageFoods._varialbleTimeValue[_systemData._foodData._indexID] = _systemData._addTimeValue;
    }


    void Update()
    {
        if (_levelUpNeedGold <= GoldManager.Instance.Gold)
            _btnImg.color = Color.white;
        else
            _btnImg.color = Color.gray;
    }


    public void LevelUpBtnClick()
    {
        if (GoldManager.Instance.Gold < _levelUpNeedGold) return;       // 돈이 없음
        GoldManager.Instance.UseGold(_levelUpNeedGold);
        CurrentSystemLevelUpDataManager.Instance.RemoveSystemDataList(_systemData);

        _systemLevelUpDel.Invoke();
    }



    public void Destroy()
    {
        Destroy(gameObject);
    }
}
