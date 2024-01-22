using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenLevelPanel : MonoBehaviour
{
    private string _savePath;

    private Kitchens _kitchens;

    public int _clickCnt = 1;
    public static int MAXCNT = 5;

    [Header("FoodData")]
    private FoodData _foodData;
    [SerializeField] TMP_Text _foodNameTxt;
    [SerializeField] Image _foodImg;


    private Slider _slider;
    [SerializeField] Button _levelUpBtn;
    private Image _btnImg;
    private TMP_Text _levelUPBtnTxt;

    [SerializeField] Image[] _starImgs;

    float _levelUpNeedGold;         // 레벨업 할 때 필요한 골드

    KitchenManager _kitchenManager => KitchenManager.Instance;

    private void Awake()
    {

        _kitchens = GetComponentInParent<Kitchens>();
        _slider = GetComponentInChildren<Slider>();
        _levelUPBtnTxt = _levelUpBtn.GetComponentInChildren<TMP_Text>();
        _btnImg = _levelUpBtn.GetComponent<Image>();

        for (int i = 0; i < _starImgs.Length; i++)
        {
            _starImgs[i].gameObject.SetActive(false);
        }

        _savePath = "kitchenPanelData" + "_" + GameManager.Instance._stage.ToString() + "_" + _kitchens._foodData._indexID.ToString();
    }

    public void InitFoodData(FoodData fooddata)
    {
        _foodData = fooddata;
        _foodNameTxt.text = _foodData.name;
        _foodImg.sprite = _foodData._sprite;

        CheckLevelUpNeedGold();
    }


    private void Update()
    {
        if (_levelUpNeedGold <= GoldManager.Instance.Gold)
        {
            _btnImg.color = Color.white;
        }
        else
            _btnImg.color = Color.gray;


    }

    /// <summary>
    /// Button Event로 연결 
    /// </summary>
    public void LevelUpBtnClick()
    {
        if (GoldManager.Instance.Gold < _levelUpNeedGold) return;       // 돈이 없음
        GoldManager.Instance.UseGold(_levelUpNeedGold);

        SoundManager.Instance.BtnClickSFXPlay();

        if (_clickCnt >= MAXCNT)
        {
            _kitchens.LevelUp();

            if (_kitchens.Level > Kitchens.MAXLEVEL)
            {
                _kitchens.Level = Kitchens.MAXLEVEL;
                _levelUpNeedGold = -1;
                _levelUpBtn.enabled = false;
                ChangeBtnText("MAX!!");
                ES3.Save(_savePath, this);
                return;
            }

            _clickCnt = 1;
            SetStarImg(_kitchens.Level - 2);        // 현재 레벨 - 1{인덱스 계산(0번부터)} - 1{ 별은 레벨 2 부터 생성}
        }
        else
        {
            _clickCnt++;
        }

        _slider.value = _clickCnt;

        _levelUpNeedGold = _kitchens._levelUpGold[_kitchens.Level - 1, _clickCnt - 1];
        ChangeBtnText(_levelUpNeedGold.ToString());

        ES3.Save(_savePath, this);

    }

    private void ChangeBtnText(string text)
    {
        _levelUPBtnTxt.text = text;
    }

    private void SetStarImg(int imgIndex)
    {
        if (imgIndex >= _starImgs.Length) return;
        _starImgs[imgIndex].gameObject.SetActive(true);
    }

    public void LoadData()
    {
        _slider.value = _clickCnt;

        if (_kitchens.Level > 1)
        {
            for (int i = 2; i <= _kitchens.Level; i++)
            {
                SetStarImg(i - 2);        // 현재 레벨 - 1{인덱스 계산(0번부터)} - 1{ 별은 레벨 2 부터 생성}
            }
        }

        CheckLevelUpNeedGold();
    }

    private void CheckLevelUpNeedGold()
    {
        if (_clickCnt >= MAXCNT && _kitchens.Level > Kitchens.MAXLEVEL)
        {
            _kitchens.Level = Kitchens.MAXLEVEL;
            _levelUpNeedGold = -1;
            _levelUpBtn.enabled = false;
            ChangeBtnText("MAX!!");
            return;
        }
        _levelUpNeedGold = _kitchens._levelUpGold[_kitchens.Level - 1, _clickCnt - 1];
        ChangeBtnText(_levelUpNeedGold.ToString());
    }
}
