using UnityEngine;
using Zenject;

public class TraderObject : InteractObject
{
    [Inject] GameModeManager _modeManager;
    public override void Intearct(bool isDown)
    {
        if(isDown)
            _modeManager.ChangeMode(EnumData.GameMode.trade);
    }
}
