using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] private LocalizedText _name;
    [SerializeField] private LocalizedText _description;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _weightText;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _dropButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TradePanel _tradePanel;

    private InventoryCell _currentCell;
    [Inject] private Inventory _inventory;

    private void Start()
    {
        _useButton.onClick.AddListener(Use);
        _dropButton.onClick.AddListener(Drop);
        _sellButton.onClick.AddListener(Sell);
    }
    public void SetItem(InventoryCell cell, bool isSell)
    {
        var item = cell.Item;
        _currentCell = cell;

        _useButton.gameObject.SetActive(!isSell);
        _dropButton.gameObject.SetActive(!isSell);
        _sellButton.gameObject.SetActive(isSell);

        if (!item)
        {
            _name.Text = "...";
            _description.Text = "....";
            _priceText.text = "0";
            _weightText.text = "0";
            _useButton.interactable = false;
            _dropButton.interactable = false;
            _sellButton.interactable = false;
            _icon.enabled = false;
            return;
        }

        _useButton.interactable = item.ItemPrefab is IUsebleItem;
        _dropButton.interactable = true;
        _sellButton.interactable = true;
        _name.Text = item.Name;
        _description.Text = item.Description;
        _priceText.text = item.Price.ToString();
        _weightText.text = item.Weight.ToString();
        _icon.enabled = true;
        _icon.sprite = item.Icon;
        if (isSell)
        {
            _tradePanel.SetItem(cell);
        }
        else
        {
            _tradePanel.gameObject.SetActive(false);
        }
    } 
    private void Use()
    {
        _inventory.UseItem(_currentCell);
    }
    private void Drop()
    {

    }
    private void Sell()
    {
        _tradePanel.Trade();
        SetItem(_currentCell, true);
    }

}
