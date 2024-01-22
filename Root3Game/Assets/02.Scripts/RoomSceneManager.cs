using System.Collections;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    SaveManager _saveManager => SaveManager.Instance;

    void Start()
    {
        SoundManager.Instance.RoomBGSoundPlay();
        StartCoroutine(WaitingLoadData());
    }

    IEnumerator WaitingLoadData()
    {
        yield return new WaitForSeconds(0.02f);
        _saveManager.LoadFurnitureData();
    }

    public void GoToStoreBtnClick()
    {
        InGameSceneManager.Instance.SceneLoadToRoomScene(SceneType.GameScene);
    }
}
