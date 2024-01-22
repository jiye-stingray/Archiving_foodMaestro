public class ServingState : State<WorkerController>
{
    KitchenManager _kitchenManager => KitchenManager.Instance;
    GoldManager _goldManager => GoldManager.Instance;

    public override void OnEnter()
    {
        // GoldManager or GameManager에 돈 추가하기 foodData 만큼
        _goldManager.AddGold(CalculateGold());

        _context._guest.Served();

        _context.ChangeState<IdleState>();
    }

    private float CalculateGold()
    {
        float addGold = 0f;

        FoodData foodData = _context._guest._foodData;
        addGold = foodData._price * _kitchenManager._kitchensList[foodData._indexID].Level;     // 현재 음식 값 * 음식 레벨 

        addGold += FoodManager.Instance.CurrentStageFoods._variableGoldValue[foodData._indexID];        // + 추가 금액 

        return addGold;
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
    }
}
