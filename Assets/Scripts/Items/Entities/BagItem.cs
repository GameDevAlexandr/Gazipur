using UnityEngine;

public class BagItem : ToolItem, IUsebleItem
{
    [SerializeField] private int _cargoValue;
    public void Use(GameManager manager)
    {
        manager.Inventory.ChangeCargoValue(_cargoValue);
    }
}
