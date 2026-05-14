using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler, IPointerClickHandler
{
    public bool IsReady { get; private set; }
    public ItemData Item { get; private set; }
    public int Count { get; private set; }
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _countText;
    [Inject] private ItemsManager _itemsManager;
    [Inject] private DataManager _data;
    [Inject] private MarketManager _market;
    [Inject] private Inventory _inventory;
    public void SetReady(bool ready) => IsReady = ready;
    private Rect _rect;
    private void Start()
    {
        _rect = _itemIcon.rectTransform.rect;
    }
    public int AddItem(ItemData item, int count)
    {
        Item = item;
        _itemIcon.enabled = true;
        _itemIcon.sprite = Item.Icon;
        int remains = Mathf.Max((Count + count) - item.MaxInInventoryCell, 0);
        Count = Mathf.Min(Item.MaxInInventoryCell, Count + count);
        _countText.text = Count.ToString();
        _inventory.ChangeCellState(this);
        return remains;
    }
    public void RemoveItem()
    {
        Item = null;
        _itemIcon.enabled = false;
        Count = 0;
        _countText.text = "";
        _inventory.ChangeCellState(this);
    }
    public void RemoveItem(int count)
    {
        if(count == Count)
        {
            RemoveItem();
            return;
        }
        Count -= count;
        _countText.text = Count.ToString();
        _inventory.ChangeCellState(this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Item) return;        
        _itemIcon.transform.SetParent(transform.parent);
        _itemIcon.transform.position = transform.position;
        _itemIcon.transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!Item) return;
        _itemIcon.transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryCell target;
        if (target = eventData.pointerDrag.GetComponent<InventoryCell>())
        {
            if (target.Item == null) 
                return;

            int cnt = target.Count;
            var itm = target.Item;
            int rem;
            if (Item == itm || !Item)
            {
                rem = AddItem(target.Item, target.Count);
                target.RemoveItem(cnt - rem);
            }
            else
            {
                target.RemoveItem();
                target.AddItem(Item, Count);
                RemoveItem();
                AddItem(itm, cnt);
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _inventory.ShowInfoPanel(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _itemIcon.transform.parent = transform;
        _itemIcon.transform.position = transform.position;
    }
}
