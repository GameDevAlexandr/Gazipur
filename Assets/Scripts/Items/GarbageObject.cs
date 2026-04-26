using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GarbageObject : InteractObject
{
    [SerializeField] private float _holdTaime = 1f;
    [SerializeField] private Vector2Int _ItemsCount = new Vector2Int(6, 10);
    [SerializeField] private Chances[] _dropChances;

    private List<ItemData> _items = new List<ItemData>();
    private int _count;
    [Inject] HoldProgressBar _holdBar;
    [Inject] Inventory _inventory;
    [System.Serializable]
    public struct Chances
    {
        public ItemData item;
        public int chance;
    }
    
    private void Start()
    {
        _count = Random.Range(_ItemsCount.x, _ItemsCount.y + 1);
        foreach (var ch in _dropChances)
        {
            for (int i = 0; i < ch.chance; i++)
            {
                _items.Add(ch.item);
            }
        }
    }
    public override void Select(bool isSelect)
    {
        if (!isSelect)
        {
            _holdBar.OnHoldComplete -= PicItem;
            Intearct(false);
        }

        base.Select(isSelect);
    }
    public override void Intearct(bool isDown)
    {
        if (isDown)
        {
            _holdBar.StartHold(_holdTaime);
            _holdBar.OnHoldComplete += PicItem;
        }
        else
        {
            _holdBar.CancelHold();
            _holdBar.OnHoldComplete -= PicItem;
        }
    }
    private void PicItem()
    {        
        int rnd = Random.Range(0, _items.Count);
        if (_inventory.AddItem(_items[rnd], 1) > 0)
            return;

        _count--;
        _items.RemoveAt(rnd);
        if (_count == 0)
        {
            _holdBar.CancelHold();
            Intearct(false);
            Destroy(gameObject);
        }        
    }
}
