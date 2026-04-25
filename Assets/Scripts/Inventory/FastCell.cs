using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FastCell : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _countText;
    [SerializeField] private Image _noUsebleImage;

    private IUsebleItem _item;
    [Inject] GameManager _manager;
    public void SetItem(ItemData item, int count)
    {
        if (!item)
        {
            _itemIcon.gameObject.SetActive(false);
            return;
        }
        _itemIcon.gameObject.SetActive(true);
        _itemIcon.sprite = item.Icon;
        _countText.text = count.ToString();
        _item = item.ItemPrefab as IUsebleItem;
        _noUsebleImage.enabled = _item==null;
    }
    public void UseItem()
    {
        if(_item!=null)
            _item.Use(_manager);
    }
}
