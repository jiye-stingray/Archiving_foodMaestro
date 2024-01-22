public abstract class State<T>
{
    protected StateMachine<T> _stateMachine;
    protected T _context;       // 실제 작업되는 controller

    public void SetMachineAndContext(StateMachine<T> stateMachine, T context)
    {
        this._stateMachine = stateMachine;
        this._context = context;
    }

    /// <summary>
    /// 상태가 시작했을 때
    /// </summary>
    public abstract void OnEnter();

    public abstract void Update();

    public abstract void OnExit();

}
