using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ItemObject : InteractObject
{
    [field: SerializeField] public ItemData Item { get; private set; }
    [field: SerializeField] public int Count { get; private set; }    
    [SerializeField] private Outline _outline;
    private MeshRenderer _mesh;
    private void Start()
    {        
        if (Item)
        {
            SetData(Item, Count);
        }
    }
    public void SetData(ItemData item, int count)
    {
        _mesh = GetComponent<MeshRenderer>();
        Count = count;
        Item = item;
        _mesh = item.Mesh;
    }
    public override void Intearct()
    {
        
    }
}
