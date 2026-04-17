using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryCell[] _cells;
    public int AddItem(ItemData item, int count)
    {
        foreach (var c in _cells)
        {            
            if (!c.IsReady) continue;

            if (c.Item == item)
                count = c.AddItem(item, count);

            if (count == 0) break;
        }
        if (count == 0) return count;

        foreach (var c in _cells)
        {
            if (!c.IsReady) continue;

            if (c.Item == null)
                count = c.AddItem(item, count);
            if (count == 0) break;
        }
        return count;
    }
}
