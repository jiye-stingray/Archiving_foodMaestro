using System.Collections.Generic;
using UnityEngine;

public class ChairManager : Singleton<ChairManager>
{
    public Queue<Chair> _chairsQueue = new Queue<Chair>();          // 대기 중인 chair 

    [SerializeField] List<Chair> _chairsList;                       // 모든 (활성화 or 비활성화 포함) chair
    public int _chairCountIndex;
    public int _chairCountSave;         // 저장할 

    public override void Awake()
    {
        base.Awake();

        // 전부 비활성화
        for (int i = 0; i < _chairsList.Count; i++)
        {
            _chairsList[i].gameObject.SetActive(false);
        }

        SaveManager.Instance.LoadChair();
    }

    private void Update()
    {
        CreateGuestCheck();
    }

    /// <summary>
    /// Guest를 초대할 수 있는지 체크한다
    /// </summary>
    private void CreateGuestCheck()
    {
        if (_chairsQueue.Count > 0)            // 빈 의자가 있다는 거임
        {
            GuestManager.Instance.CreateGuest();
        }

    }

    public Chair ReturnChair()
    {
        if( _chairsQueue.Count == 0 ) return null;       
        return _chairsQueue.Dequeue();
    }

    public void AddChair(Chair chair)
    {
        _chairsQueue.Enqueue(chair);
    }

    public void AddNewChair()
    {
        if(_chairCountIndex >= _chairsList.Count) return;

        Chair c = _chairsList[_chairCountIndex++];
        _chairCountSave++;
        SaveManager.Instance.SaveChair(_chairCountSave);
        _chairsList[_chairsList.IndexOf(c)].gameObject.SetActive(true);
        AddChair(c);
    }

    /// <summary>
    /// 시작할 때 Chair 로드
    /// Chair Count Save 증가 X 
    /// </summary>
    public void LoadAddNewChair()
    {
        Chair c = _chairsList[_chairCountIndex++];
        _chairsList[_chairsList.IndexOf(c)].gameObject.SetActive(true);
        AddChair(c);
    }
}
