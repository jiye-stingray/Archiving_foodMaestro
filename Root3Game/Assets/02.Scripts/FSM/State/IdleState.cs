using System.Collections;
using UnityEngine;



public class IdleState : State<WorkerController>
{
    GuestManager _guestManager => GuestManager.Instance;

    public override void OnEnter()
    {
        // 대기 중
        _context.StartCoroutine(FindGuest());
    }
    public override void Update()
    {

    }

    public override void OnExit()
    {

    }

    IEnumerator FindGuest()
    {
        yield return new WaitForSeconds(0.2f);      // state가 모두 생성될 때 까지 약간의 대기

        while (true)
        {

            if (GuestManager.Instance._waitingGuestQueue.Count > 0)
            {
                Guest g = _guestManager._waitingGuestQueue.Dequeue();

                if (g._worker == null)
                {
                    _context._guest = g;
                    _context.ChangeState<WalkState>();
                    yield break;
                }

            }


            yield return new WaitForEndOfFrame();

        }
    }
}
