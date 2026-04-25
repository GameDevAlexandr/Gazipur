using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public float Capacity { get; private set; }
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private InfoPanel _itemInfoPanel;
    [SerializeField] private Text _weightText;         
    [SerializeField] private InventoryCell[] _cells;
    [SerializeField] private FastCell[] _fastCells;

    private bool _isOpen;
    [Inject] DataManager _data;
    [Inject] GameModeManager _gameMode;
    private void Start()
    {
        Control.OnOpenInventory += () =>
        {
            if(_data.gameMode == EnumData.GameMode.home && !_isOpen)
            {
                ShowPanel(!_isOpen);
                _gameMode.ChangeMode(EnumData.GameMode.inventory);
            }
            else if(_data.gameMode == EnumData.GameMode.inventory && _isOpen)
            {
                ShowPanel(!_isOpen);
                _gameMode.ChangeMode(EnumData.GameMode.home);
            } 
                
        };
        Control.OnFastSlotUse += UseFastSlot;
    }
    public int AddItem(ItemData item, int count)
    {
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
        if (!isShow)
            _itemInfoPanel.Hide();
    }
    public void ChangeCellState(InventoryCell cell)
    {
        int num = System.Array.FindIndex(_cells,i=>i == cell);
        if (num < _fastCells.Length)
        {
            _fastCells[num].SetItem(cell.Item, cell.Count);
        }
        _weightText.text = GetWeight() + "/" + Capacity;
    }
    private void UseFastSlot(int number)
    {
        if (number <= _fastCells.Length)
        {
            _fastCells[number - 1].UseItem();
            if (_cells[number - 1].Item)
            {
                _cells[number - 1].RemoveItem(1);
            }
        }
    }
    public void ShowInfoPanel(InventoryCell cell)
    {
        if (cell == null)
            _itemInfoPanel.Hide();
        else
            _itemInfoPanel.Show(new string[] { cell.Item.Name, cell.Item.Description }, cell.transform.position);
    }
}
