using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Kitchens : MonoBehaviour
{
    string _savepath = string.Empty;

    [SerializeField] Transform[] _kitchenTrans;      //할당할 위치
    public int _kitchenCnt = 1;                     // 활성화  되어 있는 주방대 갯수 
    const int MAXKITCHENCNT = 4;

    private int[] _kitchenHasWorkers;                // 업무

    [SerializeField] Queue<WorkerController> _waitingWorkerQueue = new Queue<WorkerController>();

    [Header("Food")]
    public FoodData _foodData;
    [SerializeField] private int _level = 1;
    public int Level { get { return _level; }
        set {
            if (_level >= MAXLEVEL) return;
            _level = value;
        } }
    public const int MAXLEVEL = 4;

    [Header("UI")]
    public KitchenLevelPanel _panel;
    public float[,] _levelUpGold = new float[MAXLEVEL, MAXKITCHENCNT + 1]        // 최대 레벨업 만큼 필요한 골드
    {{ 1,2,3,4,5},{1,2,3,4,5 },{1,2,3,4,5 },{1,2,3,4,5 } };


    [SerializeField] AudioClip _kitchenLevelUpSound;

    private void Awake()
    {
        _savepath = "kitchenData" + "_" + GameManager.Instance._stage.ToString() + "_" + _foodData._indexID.ToString();

        _panel = GetComponentInChildren<KitchenLevelPanel>();
        _kitchenHasWorkers = new int[_kitchenTrans.Length];
    }

    private void Start()
    {
        InitKitchen();
        SaveManager.Instance.LoadKitchen(GameManager.Instance._stage,_foodData._indexID);
        PanelInit();
    }

    private void PanelInit()
    {
        _panel.gameObject.SetActive(false);
        _panel.InitFoodData(_foodData);
    }


    #region Worker Cook


    /// <summary>
    /// 요리가 가능한지 반환하는 
    /// 주방이 가득 차 있으면 요리할 수 없음
    /// </summary>
    /// <returns></returns>
    public Tuple<int, Transform> ReturnKitchen()
    {
        Tuple<int, Transform> t;

        for (int i = 0; i < _kitchenCnt; i++)
        {
            if (_kitchenHasWorkers[i] == 0)     // 비어있는 주방 (worker가 일하고 있지 않는 주방)
            {
                _kitchenHasWorkers[i] = 1;

                t = new Tuple<int, Transform>(i, _kitchenTrans[i]);
                return t;
            }
        
        }


        return null;
    }

    /// <summary>
    /// 요리가 끝났을 때 
    /// </summary>
    /// <param name="index">주방 위치</param>
    public void FinishCook(int index)
    {
        if(_waitingWorkerQueue.Count >= 1)
        {
            WorkerController worker = _waitingWorkerQueue.Dequeue();

            if(worker != null) 
                worker.ChangeState<OrderState>();
        }

        _kitchenHasWorkers[index] = 0;
    }

    public void WatingWorker(WorkerController worker)
    {
        _waitingWorkerQueue.Enqueue(worker);
        worker.ChangeState<OrderWaitingState>();
    }

    #endregion

    #region Level

    private void InitKitchen()
    {
        for (int i = 0; i < _kitchenTrans.Length; i++)
        {
            _kitchenTrans[i].gameObject.SetActive(false); 
        }
        _kitchenTrans[0].gameObject.SetActive(true);
    }

    private void AddKitchen()
    {
        if (_kitchenCnt >= MAXKITCHENCNT) return;
        _kitchenCnt++;
        _kitchenTrans[_kitchenCnt - 1].gameObject.SetActive(true);
    }



    public void LevelUp()
    {
        _level++;
        GameManager.Instance.SoundPlay(_kitchenLevelUpSound, SceneType.GameScene);
        SaveManager.Instance.SaveKitchen(_savepath,this);

        AddKitchen();
    }

    #endregion

    public void LoadKitchenData()
    {
        for (int i = 1; i < Level; i++)
        {
            AddKitchen();
        }
    }


    private void OnMouseDown()
    {
        if (GameManager.Instance._isShowSystemLevelUpPanel) return;
        InGameUIManager.Instance.ShowPanel(_panel.gameObject, !_panel.gameObject.activeSelf);

    }
}
