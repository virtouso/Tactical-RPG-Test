using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMenuSceneMainManager>().To<MenuSceneMainManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<BaseAudioManager>().To<MenuSceneAudioManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IMenuUiManager>().To<MenuUiManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IGameDataManager>().To<GameDataManager>().FromComponentsInHierarchy().AsSingle();
    }
}