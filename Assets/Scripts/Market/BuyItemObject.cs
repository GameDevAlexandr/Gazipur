using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.EventSystems;

public class BuyItemObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _priceText;
    [Inject] private MarketManager _market;
    [Inject] private Inventory _inventory;
    [Inject] private DataManager _data;
    [Inject] private Sounds _sound;
    private ItemData _item;
    private bool _isSingle;
    private int _price => (int)(_market.TraderPriceMultiplicator * _item.Price);
    public void OnPointerClick(PointerEventData eventData)
    {
        _inventory.ItemInfoPanel.SetPurchasableItem(_item);
    }

    public void SetItem(ItemData item, bool isSingle)
    {
        _isSingle = isSingle;
        _item = item;
        _itemIcon.sprite = item.Icon;
        _priceText.text = _price.ToString();
        _buyButton.onClick.AddListener(Buy);
        _buyButton.interactable = _data.Money >= _price;
        _data.onChangeMoney += () => _buyButton.interactable = _data.Money >= _price;
    }
    private void Buy()
    {
        if (_inventory.AddItem(_item, 1) > 0)
        {
            return;
        }
        if (_isSingle) gameObject.SetActive(false);
        _data.ChangeMoney(-_price);
        _sound.UIPlay(EnumData.UISound.buy);
    }
    
}
