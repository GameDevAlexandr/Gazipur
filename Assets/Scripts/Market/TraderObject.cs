using UnityEngine;
using Zenject;

public class TraderObject : InteractObject
{
    [Inject] private DialogManager _dialog;
    [Inject] private GameModeManager _gameMode;
    [Inject] private Inventory _inventory;

    private void Start()
    {
        _inventory.onTakeItem += itm =>
        {
           if(itm.ItemPrefab is ToolItem)
                _dialog.StartDialog(EnumData.DialogType.traderAfterBuy);
        };
    }
    public override void Intearct(bool isDown)
    {
        if (isDown)
        {
            if (!_dialog.StartDialog(EnumData.DialogType.startTrader))
            {
                _gameMode.ChangeMode(EnumData.GameMode.trade);
            }
        }
    }
}
