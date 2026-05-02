using UnityEngine;

public class StartTraderDialogAction : DialogAction
{
    public override void Action(GameManager manager)
    {
        manager.ModeManager.ChangeMode(EnumData.GameMode.trade);
    }
}
