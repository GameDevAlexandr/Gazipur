using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public float Capacity { get; private set; }
    [SerializeField] private InventoryCell[] _cells;
    [Inject] DataManager _data;
    public int AddItem(ItemData item, int count)
    {
        float weight = GetWeight();
        float cap = Capacity - weight;
        if (item.Weight * count > cap)
        {
            count -= (int)(cap / item.Weight);
        }
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
    public float GetWeight()
    {
        float res = 0;
        foreach (var c in _cells)
        {
            if (c.Item)
            {
                res += c.Item.Weight * c.Count;
            }
        }
        return res;
    }
}
