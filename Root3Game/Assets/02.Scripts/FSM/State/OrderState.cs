using System.Collections;
using UnityEngine;

public class OrderState : State<WorkerController>
{
    public override void OnEnter()
    {
        _context.StartCoroutine(WaitChangeState());
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {
        // 가볍게 이동.. ?할 수 도 있음
    }


    IEnumerator WaitChangeState()
    {
        GameManager.Instance.SoundPlay(_context._orderAudioClip, SceneType.GameScene);
        yield return new WaitForSeconds(1f);
        KitchenManager.Instance.CheckKitchen(_context);
        _context._guest.ShowOrder();
    }
}
