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
    protected override void PicItem()
    {
        if (!_inventory.HaveTools.Contains(_needadTool))
        {
            //Isha talk
            return;
        }
        int rnd = Random.Range(0, 100);
        if (!_inventory.HaveTools.Contains(ToolsType.glowes) && rnd >= _damageChance)
        {
            _player.TakeDamage(_damage);
        }
        base.PicItem();
    }

}
