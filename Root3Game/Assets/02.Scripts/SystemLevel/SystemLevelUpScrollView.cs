using UnityEngine;

public class SystemLevelUpScrollView : MonoBehaviour
{
    [SerializeField] RectTransform _contentRectTrans;   // content 추가할 때 마다 Height 늘리기
    [SerializeField] GameObject _systemLevelUpContentPrefab;

    public void AddLevelContent(SystemData systemData)
    {
        SystemLevelUpContent sysContent = Instantiate(_systemLevelUpContentPrefab,Vector3.zero, Quaternion.identity,_contentRectTrans).GetComponent<SystemLevelUpContent>();
        RectTransform rect = sysContent.GetComponent<RectTransform>();
        rect.localPosition = Vector3.zero;
        rect.localScale = Vector3.one;

        sysContent.Init(systemData);
        
    }

}
