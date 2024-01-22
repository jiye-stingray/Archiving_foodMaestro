using System.Collections.Generic;
using UnityEngine;

public class SystemLevelUpPanel : MonoBehaviour
{
    [SerializeField] GameObject _levelUpScrollViewObj;
    private SystemLevelUpScrollView _scrollView;


    // Temp 추후 Stage 이동할 때 받아오기
    // 저장 필요 
    private List<SystemData> _currentSystemDataList = new List<SystemData>();


    public void Awake()
    {
        _scrollView = _levelUpScrollViewObj.GetComponentInChildren<SystemLevelUpScrollView>();
    }

    private void Start()
    {
        _currentSystemDataList = CurrentSystemLevelUpDataManager.Instance._currentSystemDataList;
        for (int i = 0; i < _currentSystemDataList.Count; i++)
        {
            _scrollView.AddLevelContent(_currentSystemDataList[i]);
        }

        _levelUpScrollViewObj.SetActive(false);
    }

    private void Update()
    {
        CheckNextStage();
    }

    private void CheckNextStage()
    {
        if (GameManager.Instance._allSystemLevelUp) return;

        // data list 크기가 0 일 때 다음 스테이지 트리거 켜주기 
        if (_currentSystemDataList.Count <= 0)
        {
            GameManager.Instance._allSystemLevelUp = true;
            GameManager.Instance.NextStageCheck();
        }

    }


    public void ShowLevelUpPanelBtnClick()
    {
        SoundManager.Instance.BtnClickSFXPlay();
        
        InGameUIManager.Instance.ShowPanel(_levelUpScrollViewObj, !_levelUpScrollViewObj.activeSelf);
        GameManager.Instance._isShowSystemLevelUpPanel = !GameManager.Instance._isShowSystemLevelUpPanel;
    }
}
