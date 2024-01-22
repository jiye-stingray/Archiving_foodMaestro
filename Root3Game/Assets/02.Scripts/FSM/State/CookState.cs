using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CookState : State<WorkerController>
{

    private Image _cookImage => _context._cookTimeImage;
    private float _cookTime;

    private int _cookAnimId = Animator.StringToHash("Cook");

    public override void OnEnter()
    {
        _context._anim.SetBool(_cookAnimId, true);

        _context._cookTimeCanvasGo.SetActive(true);

        // 기본 소요 시간 - 추가 감소 시간
        _cookTime = _context._guest._foodData._cookingTime - (FoodManager.Instance.CurrentStageFoods._varialbleTimeValue[_context._guest._foodData._indexID]);

        _context.StartCoroutine(MakeFood());
    }

    public override void OnExit()
    {
        KitchenManager.Instance.FinishCook(_context, _context._kitchenIndex);
        _context._anim.SetBool(_cookAnimId, false);
        _context._cookTimeCanvasGo.SetActive(false);
    }

    public override void Update()
    {

    }

    float timer = 0f;
    IEnumerator MakeFood()
    {
        timer = 0f;

        while (timer < _cookTime)
        {
            timer += Time.deltaTime;

            _cookImage.fillAmount = (timer / _cookTime);

            yield return new WaitForEndOfFrame();
        }

        GameManager.Instance.SoundPlay(_context._cookFinishAudioClip, SceneType.GameScene);

        yield return new WaitForSeconds(0.5f);
        _context._stateMachine.ChangeState<WalkState>();
    }
}
