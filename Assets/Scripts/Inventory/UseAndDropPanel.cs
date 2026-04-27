using UnityEngine;
using UnityEngine.UI;

public class UseAndDropPanel : MonoBehaviour
{
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _dropButton;

    private InventoryCell _currCell;
    private IUsebleItem _usebleItem;
    public void SelectCell(InventoryCell cell)
    {
        transform.position = cell.transform.position;
        _currCell = cell;
        _usebleItem = cell.Item.ItemPrefab as IUsebleItem;
        _useButton.interactable = _usebleItem != null;
        gameObject.SetActive(true);
    }
}
