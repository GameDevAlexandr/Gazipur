using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MarketManager : MonoBehaviour
{
    [field: SerializeField] public float TraderPriceMultiplicator;
    [SerializeField] private BuyItemObject _buyItemPrefab;
    [SerializeField] private Transform _buyItemsPanel;
    [SerializeField] private Item[] _items;
    [Inject] private GameModeManager _modeManager;
    [Inject] private Inventory _inventory;
    [Inject] private DiContainer _container;
    [field: SerializeField] public TradePanel TradePanel;
   
    [System.Serializable]
    public struct Item
    {
        public ItemData item;
        public  bool isSingle;
    }
    private void Start()
    {
        for (int i = 0; i < _items.Length; i++)
        {
           var obj = _container.InstantiatePrefabForComponent<BuyItemObject>(_buyItemPrefab, _buyItemsPanel);
            obj.SetItem(_items[i].item,_items[i].isSingle);
        }
    }
    public void StartTrade(bool isStart)
    {
        TradePanel.gameObject.SetActive(isStart);
        _inventory.ShowPanel(isStart);
    }
}
