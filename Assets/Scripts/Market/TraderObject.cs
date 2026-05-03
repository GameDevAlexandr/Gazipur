using UnityEngine;
using Zenject;

public class TraderObject : InteractObject
{
    [Inject] DialogManager _dialog;
    public override void Intearct(bool isDown)
    {
        if (isDown)
        {
            if (!_dialog.StartDialog(EnumData.DialogType.startTrader))
            {
                _dialog.StartDialog(EnumData.DialogType.traderEveryTime);
            }
        }
    }
}
