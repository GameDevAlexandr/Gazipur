using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MarketManager : MonoBehaviour
{
    [field: SerializeField] public float TraderPriceMultiplicator;
    [SerializeField] private BuyItemObject _buyItemPrefab;
    [SerializeField] private Transform _buyItemsPanel;
    [SerializeField] private ItemData[] _items;
    [Inject] private GameModeManager _modeManager;
    [Inject] private Inventory _inventory;
    [Inject] private DiContainer _container;
    [field: SerializeField] public TradePanel TradePanel;
    private void Start()
    {
        for (int i = 0; i < _items.Length; i++)
        {
           var obj = _container.InstantiatePrefabForComponent<BuyItemObject>(_buyItemPrefab, _buyItemsPanel);
            obj.SetItem(_items[i]);
        }
    }
    public void StartSellTrade()
    {
        TradePanel.Show();
        _modeManager.ChangeMode(EnumData.GameMode.sell);
        _inventory.ShowPanel(true);
    }
    public void Exit()
    {
        TradePanel.gameObject.SetActive(false);
        _modeManager.ChangeMode(EnumData.GameMode.home);
        _inventory.ShowPanel(false);
    }
}
