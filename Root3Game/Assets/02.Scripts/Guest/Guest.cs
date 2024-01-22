using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    public FoodData _foodData;
    [HideInInspector] public Chair _chair;
    [HideInInspector] public WorkerController _worker = null;

    private PolyNavAgent _agent;
    private Animator _anim;
    int _walkId = Animator.StringToHash("Walk");

    [Header("ShowOrder")]
    [SerializeField] Image _speechBubleImg;
    [SerializeField] Image _orderFoodImg;
    private void Awake()
    {
        _agent = GetComponent<PolyNavAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Visit();
        SetFoodData();
        _speechBubleImg.gameObject.SetActive(false);
    }


    /// <summary>
    /// 주문 음식 할당
    /// </summary>
    private void SetFoodData()
    {
        int foodIndexID = FoodManager.Instance._canFoodIDList[Random.Range(0, FoodManager.Instance._canFoodIDList.Count)];
        _foodData = FoodManager.Instance.CurrentStageFoods._foodDatas[foodIndexID];
        _orderFoodImg.sprite = _foodData._sprite;
    }

    private void Visit()
    {
        Chair chair = ChairManager.Instance.ReturnChair();
        if (chair == null) Debug.LogError("자리 없음!");

        _chair = chair;
        
        // 이동 하기 (의자 방향으로)
        StartCoroutine(MoveCor(chair.gameObject.transform.position));
    }

    IEnumerator MoveCor(Vector3 vec)
    {
        _anim.SetBool(_walkId,true);

        while (true)
        {
            if(Mathf.Abs(Vector3.Distance(transform.position,vec)) <= 0.8f)
            {
                _anim.SetBool(_walkId,false);

                if(vec == GuestManager.Instance._entranceAndExitTrans.position)
                {
                    Destroy(gameObject);
                }
                else
                    GuestManager.Instance.VisitGuest(this);
                yield break;
            }

            _agent.SetDestination(vec);

            transform.eulerAngles = transform.position.x - vec.x > 0 ? Vector3.zero : new Vector3(0, 180, 0);

           yield return new WaitForEndOfFrame();
        }
    }

    public void Served()
    {
        GoldManager.Instance.ShowCoin(transform);
        _speechBubleImg.gameObject.SetActive(false);
        GuestManager.Instance.ExitGuest(this);
        _chair.EmptyChair();
        StartCoroutine(MoveCor(GuestManager.Instance._entranceAndExitTrans.position));
    }

    public void ShowOrder()
    {
        _speechBubleImg.gameObject.SetActive(true);
    }
}
