using UnityEngine;
using UnityEngine.UI;

public class WorkerController : MonoBehaviour
{
    public StateMachine<WorkerController> _stateMachine;

    public Transform _rootPrefab;                   // 내부 prefab 모델 오브젝트

    [HideInInspector] public Guest _guest;         // 접대할 손님

    public Animator _anim;
    [HideInInspector] public PolyNavAgent _agent;

    [Header("Cook")]
    public GameObject _cookTimeCanvasGo;
    public Image _cookTimeImage;    
    public int _kitchenIndex = -1;

    [Header("Audio Clip")]
    public AudioClip _orderAudioClip;
    public AudioClip _cookFinishAudioClip;
    

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<PolyNavAgent>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        
        _cookTimeCanvasGo.gameObject.SetActive(false);


        _stateMachine = new StateMachine<WorkerController>(this, new IdleState());
        _stateMachine.AddState(new OrderState());
        _stateMachine.AddState(new WalkState());
        _stateMachine.AddState(new CookState());
        _stateMachine.AddState(new OrderWaitingState());
        _stateMachine.AddState(new ServingState());

    }

    private void Update()
    {
        if (_stateMachine != null) 
        _stateMachine.Update();
    }

    public R ChangeState<R>() where R : State<WorkerController>
    {
        return _stateMachine.ChangeState<R>();
    }

    /// <summary>
    /// 접대할 손님 할당
    /// </summary>
    public void SetGuest(Guest guest)
    {
        this._guest = guest;
    }
}
