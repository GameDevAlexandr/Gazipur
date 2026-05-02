using UnityEngine;
using Zenject;

public class TraderObject : InteractObject
{
    [Inject] GameModeManager _modeManager;
    [Inject] DialogManager _dialog;
    public override void Intearct(bool isDown)
    {
        if (isDown)
        {
            _dialog.StartDialog(EnumData.DialogType.startTrader);
        }
    }
}
