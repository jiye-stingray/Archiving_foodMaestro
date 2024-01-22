using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "FoodData",menuName = "Data/Food")]
public class FoodData : ScriptableObject
{
    public string _name;
    public int _indexID;
    public Sprite _sprite;
    public float _price;
    [Tooltip("단위 : 초")]
    public float _cookingTime;
}
