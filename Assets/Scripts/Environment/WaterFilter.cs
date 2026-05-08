using UnityEngine;
using Zenject;

public class WaterFilter : InteractObject
{
    [SerializeField] private float _makeTime;
    [Inject] private DialogManager _dialog;
    [Inject] HoldProgressBar _holdBar;
    [Inject] GameModeManager _mode;

    [SerializeField] private FilterBlueprint _blueprint;
    public override void Intearct(bool isDown)
    {
        Debug.Log("filterComplete " + _blueprint.CheckComplete());
        if (!_blueprint.CheckComplete())
        {
                _dialog.Remarks.StartRemark(EnumData.RemarksType.fewParts);
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
