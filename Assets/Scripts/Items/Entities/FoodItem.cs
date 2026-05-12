using UnityEngine;
using Zenject;

public class FoodItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _count;
    public bool Use(GameManager manager)
    {
        manager.PState.Eat(_count);
        manager.Sounds.PlayerPlay(EnumData.PlayerSound.eat, false);
        return true;
    }
}
