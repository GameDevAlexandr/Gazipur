using UnityEngine;

public class FoodItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _count;
    public bool Use(GameManager manager)
    {
        manager.PState.Eat(_count);
        return true;
    }
}
