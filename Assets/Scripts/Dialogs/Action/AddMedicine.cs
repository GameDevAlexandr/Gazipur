using UnityEngine;

public class AddMedicine : DialogAction
{
    [SerializeField] private ItemData _medecine;
    public override void Action(GameManager manager)
    {
        manager.Market.AddItem(_medecine, true);
        manager.Quest.HealMother(false);
    }
}
