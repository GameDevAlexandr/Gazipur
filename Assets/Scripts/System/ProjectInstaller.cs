using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private Sounds _soundManager;
    public override void InstallBindings()
    {
        Container.Bind<Sounds>().FromInstance(_soundManager).AsSingle();
    }
}
