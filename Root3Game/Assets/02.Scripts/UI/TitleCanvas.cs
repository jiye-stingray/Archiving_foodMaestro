using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour
{

    public void StartGameBtnClick()
    {
        SceneManager.LoadScene("GameScene" + GameManager.Instance._stage.ToString());
    }
}
