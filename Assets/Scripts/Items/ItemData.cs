using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    public string Description => "d" + Name[1..]; 
    [field: SerializeField, ShowAssetPreview] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int MaxInInventoryCell { get; private set; }
    [field: SerializeField] public float Weight { get; private set; }
    [field: SerializeField] public ItemObject ItemPrefab { get; private set; }
}
