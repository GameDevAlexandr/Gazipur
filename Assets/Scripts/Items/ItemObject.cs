using UnityEngine;

public class ItemObject : InteractObject
{
    [field: SerializeField] public ItemData Item { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Outline _outline;
    private void Start()
    {
        if (Item)
        {
            SetData(Item, Count);
        }
    }
    public void SetData(ItemData item, int count)
    {
        Count = count;
        Item = item;
        _mesh = item.Mesh;
    }
    public override void Intearct()
    {
        
    }
}
