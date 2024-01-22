using UnityEngine;

public class Chair : MonoBehaviour
{    
    [SerializeField] private Transform _sitTrans;       // 실제 손님이 앉는 위치

    public void EmptyChair()
    {
        ChairManager.Instance.AddChair(this);
    }
}
