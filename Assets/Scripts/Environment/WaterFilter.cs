using UnityEngine;
using Zenject;

public class WaterFilter : InteractObject
{
    [SerializeField] private float _makeTime;
    [Inject] private Inventory _inventory;
    [Inject] private DialogManager _dialog;
    [Inject] HoldProgressBar _holdBar;
    [Inject] GameModeManager _mode;

    private FilterBlueprint _blueprint;
    public override void Intearct(bool isDown)
    {
        if (!_blueprint.CheckComplete())
        {
            if (_dialog.Remarks.StartRemark(EnumData.RemarksType.filterNeed))
            {

            }
            else
            {
                _dialog.Remarks.StartRemark(EnumData.RemarksType.fewParts);
            }
        }
        else
        {
            if (isDown)
            {
                _holdBar.StartHold(_makeTime);
                _holdBar.OnHoldComplete += Finish;
            }
            else
            {
                _holdBar.CancelHold();
                _holdBar.OnHoldComplete -= Finish;
            }
        }
    }
    private void Finish()
    {
        _mode.ChangeMode(EnumData.GameMode.die);
    }
}
