using UnityEngine;

public class WaterItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _count;
    public void Use(GameManager manager)
    {
        manager.PState.Drink(_count);
    }
}
