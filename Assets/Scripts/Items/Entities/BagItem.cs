using UnityEngine;

public class BagItem : ToolItem, IUsebleItem
{
    [SerializeField] private int _cargoValue;
    public bool Use(GameManager manager)
    {
        manager.Inventory.ChangeCargoValue(_cargoValue);
        return true;
    }
}
