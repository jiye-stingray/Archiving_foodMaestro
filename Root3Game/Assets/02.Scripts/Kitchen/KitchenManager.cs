using System;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : Singleton<KitchenManager>
{
    public List<Kitchens> _kitchensList = new List<Kitchens>();

    private Queue<Tuple<int,Transform>> _useKitchenQueue = new Queue<Tuple<int, Transform>>();

    public void CheckKitchen(WorkerController worker)
    {
        Tuple<int, Transform> t = _kitchensList[worker._guest._foodData._indexID].ReturnKitchen();

        if(t != null)
        {
            _useKitchenQueue.Enqueue(t);
            worker.ChangeState<WalkState>();
        }
        else        // 요리할 수 없음 (주방 가득 참) 
        {
            // 대기  
            _kitchensList[worker._guest._foodData._indexID].WatingWorker(worker);
        }

    }

    public Tuple<int, Transform> ReturnKitchenTrans()
    {
        return _useKitchenQueue.Dequeue();
    }

    private void Update()
    {
        NextStageCheck();
    }

    private void NextStageCheck()
    {

        if (GameManager.Instance._allKitchenLevelUp) return;

        // 모든 가구들이 전부 만랩이 되었을 때 
        for (int i = 0; i < _kitchensList.Count; i++)
        {
            // 아직 만렙이 안된 가구가 있다면 리턴
            if (_kitchensList[i].Level < KitchenLevelPanel.MAXCNT)
                return;

        }
        GameManager.Instance._allKitchenLevelUp = true;
        GameManager.Instance.NextStageCheck();      // 다음 스테이지 체크

    }

    /// <summary>
    /// 요리가 끝났을 때 
    /// Worker CookState에서 호출해줌
    /// </summary>
    /// <param name="index">주방 위치</param>
    public void FinishCook(WorkerController worker,int index)
    {
        _kitchensList[worker._guest._foodData._indexID].FinishCook(index);
    }
}
