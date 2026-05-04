using UnityEngine;
using Zenject;
using static EnumData;

public class DangerGarbageObject : GarbageObject
{
    [SerializeField] private ToolsType _needadTool;
    [SerializeField] private int _damageChance = 100;
    [SerializeField] private int _damage = 10;
    [Inject] private PlayerState _player;
    [Inject] private Inventory _inventory;
    [Inject] private DialogManager _dialog;
    protected override void PicItem()
    {
        if (!_inventory.HaveTools.Contains(_needadTool))
        {
            _dialog.Remarks.StartRemark(_needadTool == ToolsType.wrench ? RemarksType.noWrench : RemarksType.noHacksaw);
            return;
        }
        int rnd = Random.Range(0, 100);
        if (!_inventory.HaveTools.Contains(ToolsType.glowes) &&  _damageChance> rnd)
        {
            _dialog.Remarks.StartRemark(RemarksType.noGrowes);
            _player.TakeDamage(_damage);
        }
        base.PicItem();
    }

}
