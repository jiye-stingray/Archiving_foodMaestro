using System;
using UnityEngine;

public enum WalkType
{
    ToOrder,        // 주문을 받으러 손님에게 감
    ToCook,         // 요리를 하기 위해서 주방으로 감
    ToServing,      // 만든 음식을 서빙하기 위해서 손님에게 감
}

public class WalkState : State<WorkerController>
{
    int _walkId = Animator.StringToHash("Walk");
    private Vector3 vec;        // 이동하려는 목표 vector

    WalkType _walkType;

    public override void OnEnter()
    {
        _context._anim.SetBool(_walkId, true);

        InitType();
        vec = InitVec();
    }

    public override void OnExit()
    {
        _context._anim.SetBool(_walkId, false);

    }

    /// <summary>
    /// 이동 type 설정
    /// </summary>
    private void InitType()
    {
        if (_context._stateMachine.PreviousState.GetType() == typeof(IdleState))
        {
            _walkType = WalkType.ToOrder;
        }
        else if (_context._stateMachine.PreviousState.GetType() == typeof(OrderState) || _context._stateMachine.PreviousState.GetType() == typeof(OrderWaitingState))         // 주문 후 식당 대기 상태일 때의 처리도 필요 (추후)
        {
            _walkType = WalkType.ToCook;
        }
        else if (_context._stateMachine.PreviousState.GetType() == typeof(CookState))
        {
            _walkType = WalkType.ToServing;
        }
    }

    /// <summary>
    /// 목표 위치를 설정
    /// </summary>
    private Vector3 InitVec()
    {
        Vector3 v = Vector3.zero;

        switch (_walkType)
        {
            case WalkType.ToOrder:
                v = _context._guest.transform.position;
                break;

            case WalkType.ToCook:
                Tuple<int, Transform> t = KitchenManager.Instance.ReturnKitchenTrans();

                v = t.Item2.position + new Vector3(0, -0.5f, 0);
                _context._kitchenIndex = t.Item1;
                break;

            case WalkType.ToServing:
                v = _context._guest.transform.position;

                break;
            default:
                break;
        }


        return v;

    }

    public override void Update()
    {

        if (vec == Vector3.zero) return;

        if (_context._guest == null) _context.ChangeState<IdleState>();

        _context._rootPrefab.eulerAngles = _context.transform.position.x - vec.x > 0 ? Vector3.zero : new Vector3(0, 180, 0);

        _context._agent.SetDestination(vec);

        if (Vector3.Distance(_context.transform.position, vec) <= 0.8f)      // 목표에 도착했을 때 
        {
            CheckNextStage();
        }
    }

    private void CheckNextStage()
    {
        switch (_walkType)
        {
            case WalkType.ToOrder:
                _context.ChangeState<OrderState>();
                break;
            case WalkType.ToCook:
                _context.ChangeState<CookState>();
                break;
            case WalkType.ToServing:
                _context.ChangeState<ServingState>();
                break;
            default:
                break;
        }
    }


}
