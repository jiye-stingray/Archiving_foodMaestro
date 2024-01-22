using UnityEngine;

public class WorkerManager : Singleton<WorkerManager>
{
    [SerializeField] private GameObject _workerPrefab;

    public int _workerCnt;

    private void Start()
    {
        SaveManager.Instance.LoadWorker();
    }

    public void CreateWorker()
    {
        _workerCnt++;
        SaveManager.Instance.SaveWorker(_workerCnt);
        Instantiate(_workerPrefab,Vector3.zero,Quaternion.identity);
    }
}
