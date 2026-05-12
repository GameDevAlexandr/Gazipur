using UnityEngine;
using Zenject;
using static EnumData;

public class HoleInFance : InteractObject
{
    [SerializeField] private GameObject _holeFance;
    [SerializeField] private MeshRenderer _fance;
    [SerializeField] private ToolsType _tool;
    [SerializeField] private RemarksType _remark;
    [Inject] Inventory _inventory;
    [Inject] DialogManager _dialog;
    public override void Intearct(bool isDowwn)
    {
        if (_inventory.HaveTools.Contains(_tool))
        {
            _fance.enabled = false;            
            _fance.enabled = false;
            if (_holeFance)
            {
                Instantiate(_holeFance, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else
        {
            _dialog.Remarks.StartRemark(_remark);
        }
    }
}
