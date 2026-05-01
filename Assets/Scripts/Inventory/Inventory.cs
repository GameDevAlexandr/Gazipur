using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static EnumData;
public class Inventory : MonoBehaviour
{
    public HashSet<ToolsType> HaveTools { get; private set; }
    [field: SerializeField] public float Capacity { get; private set; }
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private ItemInfoPanel _itemInfoPanel;
    [SerializeField] private Text _weightText;         
    [SerializeField] private Text _inventoryWeightText;         
    [SerializeField] private InventoryCell[] _cells;
    [SerializeField] private FastCell[] _fastCells;
    [SerializeField] private PickedItemUI[] _picedItems;
    [SerializeField] private Image[] _toolsImages;

    private bool _isOpen;
    private int _picCounter;
    [Inject] DataManager _data;
    [Inject] GameModeManager _gameMode;
    [Inject] GameManager _manager;
    private void Start()
    {
        Control.OnOpenInventory += () =>
        {
            if(_data.gameMode == GameMode.outdors && !_isOpen)
            {
                _gameMode.ChangeMode(GameMode.inventory);
            }
            else if(_data.gameMode == GameMode.inventory && _isOpen)
            {
                _gameMode.ChangeMode(GameMode.outdors);
            }                 
        };
        Control.OnFastSlotUse += UseFastSlot;
    }
    public int AddItem(ItemData item, int count)
    {
        _picedItems[_picCounter % _picedItems.Length].Show(item, count);
        _picCounter++;
        if (CheckTool(item)) return 0;
        float weight = GetWeight();
        float cap = Capacity - weight;
        int res = 0;
        if (item.Weight * count > cap)
        {
            res = count - (int)(cap / item.Weight);
            count = (int)(cap / item.Weight);
        }
        foreach (var c in _cells)
        {            
            //if (!c.IsReady) continue;

            if (c.Item == item)
                count = c.AddItem(item, count);

            if (count == 0) break;
        }

        if (count != 0)
        {
            foreach (var c in _cells)
            {
                //if (!c.IsReady) continue;

                if (c.Item == null)
                    count = c.AddItem(item, count);
                if (count == 0) break;
            }
        }
        return count>res?count:res;
    }
    public float GetWeight()
    {
        float res = 0;
        foreach (var c in _cells)
        {
            if (c.Item)
            {
                res += c.Item.Weight * c.Count;
            }
        }        
        return res;
    }
    public void ShowPanel(bool isShow)
    {
        _isOpen = isShow;
        _inventoryPanel.SetActive(isShow);
        Cursor.lockState = isShow ? CursorLockMode.None : CursorLockMode.Locked;
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].Item)
            {
                ShowInfoPanel(_cells[i]);
            }
            return;
        }
        ShowInfoPanel(_cells[0]);
    }
    public void ChangeCellState(InventoryCell cell)
    {
        int num = System.Array.FindIndex(_cells,i=>i == cell);
        if (num < _fastCells.Length)
        {
            _fastCells[num].SetItem(cell.Item, cell.Count);
        }
        _weightText.text = GetWeight() + "/" + Capacity;
        _inventoryWeightText.text = _weightText.text;
    }
    private void UseFastSlot(int number) => UseItem(_cells[number - 1]);

    public void UseItem(InventoryCell cell)
    {
        IUsebleItem item = cell.Item.ItemPrefab as IUsebleItem;
        if (item != null)
        {
            item.Use(_manager);
            cell.RemoveItem(1);
        }
    }
    public void ShowInfoPanel(InventoryCell cell)
    {
        _itemInfoPanel.SetItem(cell, _data.gameMode == GameMode.trade);
    }
    public bool CheckTool(ItemData item)
    {
        HaveTools ??= new HashSet<ToolsType>();
        ToolItem ti = item.ItemPrefab as ToolItem;
        if (ti)
        {
            HaveTools.Add(ti.ToolType);
            _toolsImages[(int)ti.ToolType].sprite = item.Icon;
            return true;
        }
        return false;
        
    }
}
