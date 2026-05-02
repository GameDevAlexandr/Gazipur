using UnityEngine;
using Zenject;

public class DangerGarbageObject : GarbageObject
{
    [SerializeField] private int _damageChance = 100;
    [SerializeField] private int _damage = 10;
    [Inject] private PlayerState _player;
    [Inject] private Inventory _inventory;
    protected override void PicItem()
    {
        int rnd = Random.Range(0, 100);
        if (!_inventory.HaveTools.Contains(EnumData.ToolsType.glowes) && rnd >= _damageChance)
        {
            _player.TakeDamage(_damage);
        }
        base.PicItem();
    }

}
