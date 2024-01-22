using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    SoundManager _soundManager => SoundManager.Instance;

    [Header("Stage")]
    public int _stage = 1;      // 현재 스테이지
    public const int MAXSTAGE = 2;      // 최대 스테이지
    public bool _allSystemLevelUp;       // 모든 시스템을 레벨업 시켰을 때
    public bool _allKitchenLevelUp;      // 모든 주방 max 레벨업 

    public bool _isShowSystemLevelUpPanel;

    private void Start()
    {
        SaveManager.Instance.LoadStage();
        _soundManager.InGameBGSoundPlay();
    }

    public void SoundPlay(AudioClip clip,SceneType sceneType)
    {
        if (sceneType != InGameSceneManager._currentGameScene) return;
        SoundManager.Instance.SFXPlay(clip);
    }

    public void NextStageCheck()
    {
        if (_stage < MAXSTAGE && (_allSystemLevelUp && _allKitchenLevelUp))
        {
            InGameUIManager.Instance.ShowNextStageBtn(); 
        }
    }

    public void MoveNextStage()
    {
        _stage++;       // Stage 저장 처리 필요
        SaveManager.Instance.SaveStage();

        if (_stage > MAXSTAGE)
        {
            _stage = MAXSTAGE;
            SaveManager.Instance.SaveStage();
            return;
        }

        _isShowSystemLevelUpPanel = false;

        SceneManager.LoadScene("GameScene" + _stage.ToString());
        StartCoroutine(WaitingNewStageSceneLoadCor());
    }

    IEnumerator WaitingNewStageSceneLoadCor()
    {
        yield return new WaitForSeconds(0.01f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene" + _stage.ToString()));
        // 새로운 스테이지에서 카메라 다시 할당
        InGameSceneManager.Instance.SetMainCam();
    }
}
