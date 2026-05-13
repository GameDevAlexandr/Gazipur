using UnityEngine;
using UnityEngine.VFX;
using Zenject;
using static EnumData;

public class OpenGate : InteractObject
{
    [SerializeField] private ToolsType _tool;
    [SerializeField] private RemarksType _remark;
    [SerializeField] private VisualEffect _effect;

    [Inject] Inventory _inventory;
    [Inject] DialogManager _dialog;

    Animator _animator;
    BoxCollider _boxCollider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public override void Intearct(bool isDowwn)
    {
        if (_inventory.HaveTools.Contains(_tool))
        {
            _animator.enabled = true;
            _effect.enabled = false;
            _boxCollider.enabled = false;
        }

        else
        {
            _dialog.Remarks.StartRemark(_remark);
        }
    }
}