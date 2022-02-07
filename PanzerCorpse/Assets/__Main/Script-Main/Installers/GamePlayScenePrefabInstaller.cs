using Panzers.Configurations;
using Panzers.Configurations.Panzers.Configurations;
using Panzers.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GamePlayScenePrefabInstaller : MonoInstaller
{
    [SerializeField] private FightingUnitsList _fightingUnitsListReference;
    [SerializeField] private HexPanel _hexPanelPrefab;
    [SerializeField] private HealthBarViewModel _healthBarPrefab;
    [SerializeField] private UnitInitialPlacementConfig _placementConfig;
  
    [SerializeField] private MatchGeneralSettings _matchGeneralSettings;

    public override void InstallBindings()
    {
        Container.Bind<IMatchGeneralSettings>().To<MatchGeneralSettings>().FromScriptableObject(_matchGeneralSettings).AsSingle().NonLazy();
        Container.Bind<HexPanelBase>().To<HexPanel>().FromInstance(_hexPanelPrefab).AsSingle();
        Container.BindFactory<HealthBarViewModel, HealthBarViewModel.Factory>().FromComponentInNewPrefab(_healthBarPrefab).AsTransient();
        Container.Bind<TowerBase>().WithId("Player").To<Tower>().FromInstance(_fightingUnitsListReference.playerTowerBase).AsCached();
        Container.Bind<TowerBase>().WithId("Opponent").To<Tower>().FromInstance(_fightingUnitsListReference.enemyTowerBase).AsCached();
        Container.Bind<IFightingUnitsList>().To<FightingUnitsList>().FromScriptableObject(_fightingUnitsListReference).AsSingle();
        Container.Bind<IUnitInitialPlacementConfig>().To<UnitInitialPlacementConfig>().FromScriptableObject(_placementConfig).AsSingle();
    }


 
}