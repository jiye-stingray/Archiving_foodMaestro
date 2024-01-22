using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    GameScene,
    RoomScene
}

/// <summary>
/// RoomScene 으로 넘어갈때의 처리
/// </summary>
public class InGameSceneManager : Singleton<InGameSceneManager>
{
    public static SceneType _currentGameScene = SceneType.GameScene;      // 시작은 GameScene
    private Camera _mainCam;


    void Start()
    {
        SetMainCam();
    }

    /// <summary>
    /// 기존 카메라는 시작하는 스테이지 (ex. 1) 
    /// 에서 할당되기 때문에 다른 스테이지 (ex. 2)
    /// 로 넘어간다면 접근할 수 없다.
    /// </summary>
    public void SetMainCam()
    {
        _mainCam = Camera.main;
    }

    /// <summary>
    /// 룸씬에서 게임씬으로 
    /// 또는 게임씬에서 룸씬으로 전환
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoadToRoomScene(SceneType sceneName)
    {

        if (_currentGameScene.Equals(sceneName)) return;

        _mainCam.gameObject.SetActive(false);
        _currentGameScene = sceneName;
        

        if(_currentGameScene == SceneType.RoomScene)
        {
            StartCoroutine(LoadingSceneCor());
        }
        else
        {
            SceneManager.UnloadSceneAsync("RoomScene");         // Room Scene 비활성화
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene" + GameManager.Instance._stage.ToString()));
            SoundManager.Instance.InGameBGSoundPlay();

            _mainCam.gameObject.SetActive(true);  
        }

    }

    /// <summary>
    /// Active Scene 할당을 위한 대기 처리
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingSceneCor()
    {
        SoundManager.Instance.RoomBGSoundPlay();
        AsyncOperation async =  SceneManager.LoadSceneAsync(_currentGameScene.ToString(), LoadSceneMode.Additive);       // Room Scene 로드
        yield return new WaitUntil(() => async.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentGameScene.ToString()));
    }
}
