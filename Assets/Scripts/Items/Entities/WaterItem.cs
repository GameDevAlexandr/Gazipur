using UnityEngine;

public class WaterItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _thirsRemoveCount;
    public void Use(GameManager manager)
    {
        manager.PState.Drink(_thirsRemoveCount);
    }
}
