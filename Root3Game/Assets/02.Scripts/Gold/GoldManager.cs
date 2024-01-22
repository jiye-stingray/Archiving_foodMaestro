using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class GoldManager : Singleton<GoldManager>
{

    private GoldInfoUI _goldInfoUI;

    private float _gold;
    public float Gold {  get { return _gold; } }

    #region Gold Effect
    [SerializeField] private GameObject _effectCoinPrefab;
    private IObjectPool<GameObject> _pool;
    [SerializeField] RectTransform _canvasRect;

    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private RectTransform _goldImgRectTrans;
    [SerializeField] private Vector2[] _initialPos;
    [SerializeField] private Quaternion[] _initialRotation;
    [SerializeField] private int _coinsAmount;

    [SerializeField] Camera _uiCamera;
    #endregion

    [SerializeField] AudioClip _goldAudioClip;

    public override void Awake()
    {
        base.Awake();
        FindGoldInfoUI();

        // GoldInfoUI의 CurrentGold Check를 위해 Awake에서 Gold 할당 필요
        SaveManager.Instance.LoadGold();
    }

    void Start()
    {
        #region Gold Effect

        if (_coinsAmount == 0)
            _coinsAmount = 10; // 동전 effect의 갯수 기반으로 값 변경 필요

        _initialPos = new Vector2[_coinsAmount];
        _initialRotation = new Quaternion[_coinsAmount];

        for (int i = 0; i < _effectCoinPrefab.transform.childCount; i++)
        {
            _initialPos[i] = _effectCoinPrefab.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            _initialRotation[i] = _effectCoinPrefab.transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }

        PoolInit();

        #endregion
    }

    #region Pool

    private void PoolInit()
    {
        _pool = new ObjectPool<GameObject>(CreatePoolItem, OnTakeFromPool, OnReturnToPool,
            OnDestroyPoolObject, true, 4, 10);

        for (int i = 0; i < 10; i++)
        {
            _pool.Release(CreatePoolItem());
        }
    }

    /// <summary>
    /// 생성
    /// </summary>
    private GameObject CreatePoolItem()
    {
        GameObject go = Instantiate(_effectCoinPrefab);

        RectTransform rect = go.GetComponent<RectTransform>();

        rect.SetParent(_canvasRect);
        rect.localScale = Vector3.one;

        return go;

    }

    /// <summary>
    /// 사용
    /// </summary>
    private void OnTakeFromPool(GameObject go)
    {
        go.SetActive(true);
    }

    /// <summary>
    /// 반환
    /// </summary>
    private void OnReturnToPool(GameObject go)
    {
        go.SetActive(false);
    }

    /// <summary>
    /// 삭제
    /// </summary>
    private void OnDestroyPoolObject(GameObject go)
    {
        Destroy(go);
    }

    #endregion


    private void FindGoldInfoUI()
    {
        _goldInfoUI = FindObjectOfType<GoldInfoUI>();
    }

    public void AddGold(float gold)
    {
        _gold += gold;
        ES3.Save("gold", _gold);
        _goldInfoUI.ChangeGoldTxt(_gold);
    }

    public void UseGold(float gold)
    {
        if (_gold < gold) return;       // 돈이 적어서 사용할 수 없음
        _gold -= gold;
        ES3.Save("gold", _gold);
        _goldInfoUI.ChangeGoldTxt(_gold);
    }

    public void LoadSetGold(float goldValue)
    {
        _gold = goldValue;
        _goldInfoUI.LoadSetGoldText(_gold);
    }

    #region GoldParticle



    public void ShowCoin(Transform trans)
    {
        GameManager.Instance.SoundPlay(_goldAudioClip, SceneType.GameScene);
        StartCoroutine(CoinMoveCor(trans));

    }

    IEnumerator CoinMoveCor(Transform trans)
    {
        GameObject effectCoin = _pool.Get();

        RectTransform rect = effectCoin.GetComponent<RectTransform>();

        Vector3 screenPosition  = Camera.main.WorldToScreenPoint(trans.position);
        rect.position = _uiCamera.ScreenToWorldPoint(screenPosition);

        Reset(effectCoin);

        var delay = 0f;

        

        for (int i = 0; i < effectCoin.transform.childCount; i++)
        {
            effectCoin.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            effectCoin.transform.GetChild(i).GetComponent<RectTransform>().DOMove(_goldImgRectTrans.position, 0.8f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);


            effectCoin.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash);


            effectCoin.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;

            _counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);

        }

        yield return new WaitForSeconds(3f);
        
        _pool.Release(effectCoin);
    }

    private void Reset(GameObject coin)
    {
        for (int i = 0; i < coin.transform.childCount; i++)
        {
            coin.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = _initialPos[i];
            coin.transform.GetChild(i).GetComponent<RectTransform>().rotation = _initialRotation[i];
        }
    }

    #endregion

}
