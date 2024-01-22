using DG.Tweening;
using TMPro;
using UnityEngine;

public class GoldInfoUI : MonoBehaviour
{
    private float _currentGold;
    [SerializeField] private TMP_Text _goldText;

    private Tween _tween;

    private void Start()
    {
        _currentGold = GoldManager.Instance.Gold;
        ChangeGoldTxt(_currentGold);
    }


    public void ChangeGoldTxt(float gold)
    {
        _tween.Kill(true);

        float temp = _currentGold;
        _currentGold = gold;
        if (temp < gold)
        {
            _tween = DOTween.To(() => temp, x => temp = x, gold, 1f)
                .SetDelay(1f)
                .SetEase(Ease.Linear) // 선형 보간
                .OnUpdate(() => _goldText.text = ((int)temp).ToString())
                .OnComplete(() =>
                {
                    _goldText.text = gold.ToString();
                }
                ); // 매 프레임마다 호출되는 콜백


        }
        else
        {
            _goldText.text = _currentGold.ToString();
        }


    }
    public void LoadSetGoldText(float gold)
    {
        _goldText.text = gold.ToString();
    }

}

