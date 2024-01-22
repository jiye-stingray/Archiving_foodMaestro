using DG.Tweening;
using UnityEngine;

public class InGameUIManager : Singleton<InGameUIManager>
{
    [SerializeField] GameObject _nextStageBtn;
    public static GameObject _panelObj;        // 현재 활성화 되어있는 오브젝트


    private void Start()
    {

        _nextStageBtn.SetActive(false);
    }

    public void ShowPanel(GameObject panelObj, bool isActive)
    {
        if(_panelObj != null && _panelObj != panelObj && _panelObj.activeSelf)     // 기존의 열려있던 panel를 닫아주는 작업 
        {
            _panelObj.SetActive(false);
            _panelObj = panelObj;
        }
        else
        {
            _panelObj = panelObj;
        }

        if(isActive)
        {
            DOTween.Kill(panelObj.transform,true);
            panelObj.transform.localScale = Vector3.one * 0.9f;
            panelObj.transform.DOScale(1, 0.95f)
                .SetEase(Ease.OutElastic);
        }
        
        _panelObj.SetActive(isActive);
    }

    public void GotoRoomBtnClick()
    {
        InGameSceneManager.Instance.SceneLoadToRoomScene(SceneType.RoomScene);
    }

    #region Stage

    public void ShowNextStageBtn()
    {
        _nextStageBtn.SetActive(true);


        _nextStageBtn.transform.localScale = Vector3.one * 0.9f;
        _nextStageBtn.transform.DOScale(1, 0.95f)
            .SetEase(Ease.OutElastic);
    }

    public void NextStageBtnClick()
    {
        GameManager.Instance.MoveNextStage();
    }



    #endregion

}
