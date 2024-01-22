using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private T _context;         // 상태 관리의 controller

    private State<T> _currentState = null;     // 현재 State
    private State<T> _previousState = null;    // 이전 State
    private float _elapsedTimeInState;  // State 유효 시간 

    private Dictionary<System.Type, State<T>> _states = new Dictionary<System.Type, State<T>>();        // 상태 모음
    public State<T> CurrentState { get { return _currentState; } }
    public State<T> PreviousState { get { return _previousState; } }
    public float ElapsedTimeInState { get { return _elapsedTimeInState; } }

    public StateMachine(T context, State<T> initialState)
    {
        this._context = context;
        AddState(initialState);
        _currentState = initialState;
        _currentState.OnEnter();
    }

    /// <summary>
    /// State Dictionary에 추가
    /// </summary>
    /// <param name="state">추가할 State</param>
    public void AddState(State<T> state)
    {
        state.SetMachineAndContext(this, _context);
        _states[state.GetType()] = state;
    }

    public void Update()
    {
        _elapsedTimeInState += Time.deltaTime;
        _currentState.Update();
    }

    /// <summary>
    /// 상태 변경 함수
    /// </summary>
    /// <typeparam name="R">State</typeparam>
    /// <returns></returns>
    public R ChangeState<R>() where R : State<T>
    {
        var newType = typeof(R);

        if(CurrentState.GetType() == newType)       // 현재 상태와 변경 예정 상태가 똑같을 때 
        {
            return _currentState as R;
        }
        if(_currentState != null)                   
        {
            _currentState.OnExit();                 // 현재 상태 종료
        }

        _previousState = _currentState;
        _currentState = _states[newType];
        _currentState.OnEnter();

        _elapsedTimeInState = 0.0f;

        return _currentState as R;
    }
}
