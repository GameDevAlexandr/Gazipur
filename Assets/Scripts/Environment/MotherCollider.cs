using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider))]

public class MotherCollider : MonoBehaviour
{
    [Inject] Inventory _inventory;
    [Inject] DialogManager _dialog;
    [Inject] QuestManager _quest;
    private bool _isWork;
    private void Start()
    {
        _inventory.onTakeItem += itm => _isWork = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isWork) return;

        if (other.GetComponent<PlayerMovement>())
        {
            if (!_inventory.HaveTools.Contains(EnumData.ToolsType.crowbar))
            {
                _dialog.Remarks.StartRemark(EnumData.RemarksType.tooEarly);
            }
            else
            {
                if(!_dialog.StartDialog(EnumData.DialogType.motherDisease))
                {
                    _dialog.Remarks.StartRemark(EnumData.RemarksType.relaxMom);
                }
            }
            var medCell = _inventory.CheckMedeicine();
            if (medCell != null)
            {
                _dialog.StartDialog(EnumData.DialogType.motherMedecine);
                medCell.RemoveItem();
                _quest.HealMother(true);
            }
        }
    }

}
