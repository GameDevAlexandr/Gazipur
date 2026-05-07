using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider))]

public class MotherCollider : MonoBehaviour
{
    [Inject] Inventory _inventory;
    [Inject] DialogManager _dialog;
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
            if (!_inventory.HaveTools.Contains(EnumData.ToolsType.wrench))
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
        }
    }

}
