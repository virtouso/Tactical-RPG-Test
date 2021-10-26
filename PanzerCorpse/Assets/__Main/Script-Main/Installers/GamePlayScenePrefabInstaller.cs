using UnityEngine;
using Zenject;

public class GamePlayScenePrefabInstaller : MonoInstaller
{
    [SerializeField] private HexPanel _hexPanelPrefab;
    public override void InstallBindings()
    {
        Container.Bind<HexPanelBase>().To<HexPanel>().FromInstance(_hexPanelPrefab).AsSingle();
    }
}