using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "FurnitureData", menuName = "Data/Furniture")]
public class FurnitureData : ScriptableObject
{
    public string _name;
    public int _index;
    public Sprite _sprite;
}
