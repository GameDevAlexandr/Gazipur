using UnityEngine;

public class FoodItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _count;
    public void Use(GameManager manager)
    {
        manager.PState.Eat(_count);
    }
}
