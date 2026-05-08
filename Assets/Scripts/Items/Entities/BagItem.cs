using UnityEngine;

public class BagItem : ToolItem, IUsebleItem
{
    [SerializeField] private int _cargoValue;
    bool IUsebleItem.Use(GameManager manager)
    {
        manager.Inventory.ChangeCargoValue(_cargoValue);
        return true;
    }
}
