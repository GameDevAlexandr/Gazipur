using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public string Name; 
    [field: SerializeField, TextArea] public string Description; 
    [field: SerializeField, ShowAssetPreview] public Sprite Icon; 
    [field: SerializeField] public int Price; 
    [field: SerializeField] public int MaxInInventoryCell; 
    [field: SerializeField] public MeshRenderer Mesh; 
}
