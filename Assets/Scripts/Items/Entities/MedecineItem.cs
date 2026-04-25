using UnityEngine;

public class MedecineItem : ItemObject, IUsebleItem
{
    [SerializeField] private int _count;
    public void Use(GameManager manager)
    {
        manager.PState.Heal(_count);
    }
}
