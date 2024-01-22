using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class ScrollerController : MonoBehaviour , IEnhancedScrollerDelegate
{
    public FurnitureButton _buttonPrefab;
    public EnhancedScroller _scroller;

    private void Awake()
    {
        _scroller.Delegate = this;
        _scroller.ReloadData();
    }

    public void ReloadData()
    {
        
        _scroller.ReloadData();
        
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        FurnitureButton button =  scroller.GetCellView(_buttonPrefab) as FurnitureButton;
        button.Init(FurnitureManager.Instance._furnitureDataList[dataIndex]);

        return button;

    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return FurnitureManager.Instance._furnitureDataList.Count;
    }
}
