using UnityEngine;

public class WaterItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _thirsRemoveCount;
    public bool Use(GameManager manager)
    {
        manager.PState.Drink(_thirsRemoveCount);
        return true;
    }
}
