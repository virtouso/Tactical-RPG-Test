using UnityEngine;
using Zenject;

public class GamePlaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.Bind<IHexMapGenerator>().To<HexMapGenerator>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IGamePlayMainManager>().To<GamePlayMainManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IGameStateManager>().To<GameStateManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IGameDataManager>().To<GameDataManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IGamePlayUiManager>().To<GamePlayUiManager>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<BaseAudioManager>().To<GamePlaySceneAudioManager>().FromComponentsInHierarchy().AsSingle();
    }
}