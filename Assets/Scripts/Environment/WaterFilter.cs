using UnityEngine;
using Zenject;

public class WaterFilter : InteractObject
{
    [SerializeField] private float _makeTime;
    [Inject] private DialogManager _dialog;
    [Inject] private QuestManager _quest;
    [Inject] HoldProgressBar _holdBar;

    [SerializeField] private FilterBlueprint _blueprint;
    public override void Intearct(bool isDown)
    {
        Debug.Log("filterComplete " + _blueprint.CheckComplete());
        if (!_blueprint.CheckComplete())
        {
                _dialog.Remarks.StartRemark(EnumData.RemarksType.fewParts);
        }
        else if(_quest.QuestsState[EnumData.Quests.healMother] == 2)
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
        else
        {
            _dialog.Remarks.StartRemark(EnumData.RemarksType.firstMother);
        }
    }
    private void Finish()
    {
        _quest.CompleteFilter();
    }
}
