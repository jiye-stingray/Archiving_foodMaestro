using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoImg : MonoBehaviour
{
    private Image _logoImg;

    private void Awake()
    {
        _logoImg = GetComponent<Image>();
    }

    private void Start()
    {
        _logoImg.color = new Color(1, 1, 1, 0);
        StartCoroutine(RogoWaitingCor());
    }

    /// <summary>
    /// 로고씬 연출
    /// </summary>
    /// <returns></returns>
    IEnumerator RogoWaitingCor()
    {
        

        _logoImg.DOFade(1, 2f)
            .SetEase(Ease.Linear);

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("TitleScene");

    }
}
