using UnityEngine;
using Zenject;

public class GamePlaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BaseOpponent>().To<AiOpponentBasedOnChainOfResponsibility>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IEndPopup>().To<EndPopup>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITurnPopup>().To<TurnPopup>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputHandlerBase>().To<WindowsInputHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGamePlayCamera>().To<GamePlayCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGamePlayMainManager>().To<GamePlayMainManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameDataManager>().To<GameDataManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGamePlayUiManager>().To<GamePlayUiManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BaseAudioManager>().To<GamePlaySceneAudioManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IOpponentManager>().To<OpponentManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IHexMapGenerator>().To<HexMapGenerator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IMapElementsGenerator>().To<MapElementsGenerator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameStateManager>().To<GameStateManager>().FromComponentInHierarchy().AsSingle();
    }
}