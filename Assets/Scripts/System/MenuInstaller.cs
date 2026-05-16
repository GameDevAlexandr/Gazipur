using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private Sounds _sounds;
    [SerializeField] private MainMenuScript _mainMenu;

    public override void InstallBindings()
    {
        Container.Bind<Sounds>().FromInstance(_sounds).AsSingle();
        Container.Bind<MainMenuScript>().FromInstance(_mainMenu).AsSingle();
    }
}