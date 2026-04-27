using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [SerializeField] private Control _controll;
    [SerializeField] private DataManager _data;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameModeManager _gameModeManager;
    [SerializeField] private CraftManager _craftManager;
    [SerializeField] private ItemsManager _itemsManager;
    [SerializeField] private MarketManager _market;
    [SerializeField] private PlayerState _playerState;
    [SerializeField] private GameManager _manager;
    [SerializeField] private HoldProgressBar _holdProgress;
    [SerializeField] private PlayerMovement _player;
    public override void InstallBindings()
    {
        Container.Bind<Control>().FromInstance(_controll).AsSingle();
        Container.Bind<Inventory>().FromInstance(_inventory).AsSingle();
        Container.Bind<GameModeManager>().FromInstance(_gameModeManager).AsSingle();
        Container.Bind<CraftManager>().FromInstance(_craftManager).AsSingle();
        Container.Bind<ItemsManager>().FromInstance(_itemsManager).AsSingle();
        Container.Bind<DataManager>().FromInstance(_data).AsSingle();
        Container.Bind<MarketManager>().FromInstance(_market).AsSingle();
        Container.Bind<GameManager>().FromInstance(_manager).AsSingle();
        Container.Bind<PlayerState>().FromInstance(_playerState).AsSingle();
        Container.Bind<HoldProgressBar>().FromInstance(_holdProgress).AsSingle();
        Container.Bind<PlayerMovement>().FromInstance(_player).AsSingle();
    }
}