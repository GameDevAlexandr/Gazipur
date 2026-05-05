using UnityEngine;
using Zenject;

public class TraderObject : InteractObject
{
    [Inject] private DialogManager _dialog;
    [Inject] private GameModeManager _gameMode;
    public override void Intearct(bool isDown)
    {
        if (isDown)
        {
            if (!_dialog.StartDialog(EnumData.DialogType.startTrader))
            {
                if (!_dialog.StartDialog(EnumData.DialogType.traderAfterBuy))
                    _gameMode.ChangeMode(EnumData.GameMode.trade);
            }
        }
    }
}
