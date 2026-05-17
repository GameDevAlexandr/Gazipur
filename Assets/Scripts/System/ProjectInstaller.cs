using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SoundControl _soundControl;
    public override void InstallBindings()
    {
        Container.Bind<SoundControl>().FromInstance(_soundControl).AsSingle();
    }
}
