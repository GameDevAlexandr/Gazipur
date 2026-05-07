using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static EnumData;
public class Inventory : MonoBehaviour
{
    public System.Action<ItemData> onTakeItem;
    public HashSet<ToolsType> HaveTools { get; private set; }
    [field: SerializeField] public float Capacity { get; private set; }
    [field: SerializeField] public ItemInfoPanel ItemInfoPanel { get; private set; }
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private FilterBlueprint _filterBlueprint;
    [SerializeField] private Text _weightText;         
    [SerializeField] private Text _inventoryWeightText;         
    [SerializeField] private Text _cargoPriceText;         
    [SerializeField] private Text _inventoryCargoPriceText;         
    [SerializeField] private InventoryCell[] _cells;
    [SerializeField] private FastCell[] _fastCells;
    [SerializeField] private PickedItemUI[] _picedItems;
    [SerializeField] private Image[] _toolsImages;

    private bool _isOpen;
    private int _picCounter;
    private int _cargoPrice;
    [Inject] DataManager _data;
    [Inject] GameModeManager _gameMode;
    [Inject] GameManager _manager;
    [Inject] DialogManager _dialog;
    private void Start()
    {
        HaveTools = new HashSet<ToolsType>();
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
        _picCounter++;
        int startCount = count;
        onTakeItem?.Invoke(item);
        if (CheckTool(item))
        {
            _picedItems[_picCounter % _picedItems.Length].Show(item, count);
            return 0;
        }

        if (CheckFilterBlueprint(item))
        {
            _picedItems[_picCounter % _picedItems.Length].Show(item, count);
            return 0;
        }

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
        res = count > res ? count : res;
        if (res < startCount)
        {
            _picedItems[_picCounter % _picedItems.Length].Show(item, count);
        }
        else
        {
            _dialog.Remarks.StartRemark(RemarksType.inventoryFool);
        }
        return res;
    }
    public float GetWeight()
    {
        float res = 0;
        _cargoPrice = 0;
        foreach (var c in _cells)
        {
            if (c.Item)
            {
                res += c.Item.Weight * c.Count;
                _cargoPrice += c.Item.Price * c.Count;
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
        ChangeCargoValue(Capacity);
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
        ItemInfoPanel.SetItem(cell, _data.gameMode == GameMode.trade);
    }
    public bool CheckTool(ItemData item)
    {
        ToolItem ti = item.ItemPrefab as ToolItem;
        if (ti)
        {
            HaveTools.Add(ti.ToolType);
            _toolsImages[(int)ti.ToolType].sprite = item.Icon;
            IUsebleItem use = item as IUsebleItem;

            if (use!=null)
                use.Use(_manager);

            return true;
        }
        return false;        
    }
    public bool CheckFilterBlueprint(ItemData item)
    {
        FilterPart fp = item.ItemPrefab as FilterPart;
        if (fp)
        {
            _filterBlueprint.AddPart(fp.Part);
            return true;
        }
        return false;
    }
    public void ChangeCargoValue(float value)
    {
        if (value < Capacity) return;
        Capacity = value;
        _weightText.text = GetWeight() + "/" + Capacity;
        _inventoryWeightText.text = _weightText.text;
        _cargoPriceText.text = _cargoPrice.ToString();
       _inventoryCargoPriceText.text = _cargoPrice.ToString();
    }
}
