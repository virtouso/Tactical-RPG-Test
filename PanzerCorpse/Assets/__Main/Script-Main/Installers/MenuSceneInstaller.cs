using Panzers.Manager;
using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMenuSceneMainManager>().To<MenuSceneMainManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BaseAudioManager>().To<MenuSceneAudioManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IMenuUiManager>().To<MenuUiManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameDataManager>().To<GameDataManager>().FromComponentInHierarchy().AsSingle();
    }
}