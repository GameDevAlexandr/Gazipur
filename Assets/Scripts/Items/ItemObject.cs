using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData Item { get; private set; }
    public int Count { get; private set; }
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Outline _outline;
    public void SetData(ItemData item, int count)
    {
        Count = count;
        Item = item;
        _mesh = item.Mesh;
    }
    public void ShowOutline(bool isShow) => _outline.enabled = isShow;
}
