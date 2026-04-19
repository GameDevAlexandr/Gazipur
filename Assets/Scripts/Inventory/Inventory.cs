using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryCell[] _cells;
    [Inject] DataManager _data;
    public int AddItem(ItemData item, int count)
    {
        int res;
        foreach (var c in _cells)
        {            
            if (!c.IsReady) continue;

            if (c.Item == item)
                count = c.AddItem(item, count);

            if (count == 0) break;
        }

        if (count != 0)
        {
            foreach (var c in _cells)
            {
                if (!c.IsReady) continue;

                if (c.Item == null)
                    count = c.AddItem(item, count);
                if (count == 0) break;
            }
        }
        return count;
    }
}
