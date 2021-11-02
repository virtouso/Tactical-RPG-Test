using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GamePlayScenePrefabInstaller : MonoInstaller
{
    [SerializeField] private FightingUnitsList _fightingUnitsListReference;
    [SerializeField] private HexPanel _hexPanelPrefab;
    [SerializeField] private HealthBarViewModel _healthBarPrefab;
    [SerializeField] private UnitInitialPlacementConfig _placementConfig;
    [SerializeField] private Transform _healthBarCanvas;
    [SerializeField] private MatchGeneralSettings _matchGeneralSettings;
    [SerializeField] private TurnPopup _turnPopup;
    [SerializeField] private EndPopup _endPopup;
    public override void InstallBindings()
    {
        Container.Bind<IEndPopup>().To<EndPopup>().FromInstance(_endPopup);
        Container.Bind<ITurnPopup>().To<TurnPopup>().FromInstance(_turnPopup);
        Container.Bind<HexPanelBase>().To<HexPanel>().FromInstance(_hexPanelPrefab).AsSingle();
        Container.BindFactory<HealthBarViewModel, HealthBarViewModel.Factory>()
            .FromComponentInNewPrefab(_healthBarPrefab).AsTransient();
        Container.Bind<IMatchGeneralSettings>().To<MatchGeneralSettings>().FromScriptableObject(_matchGeneralSettings)
            .AsSingle();

        Container.Bind<TowerBase>().WithId("Player").To<Tower>()
            .FromInstance(_fightingUnitsListReference.playerTowerBase).AsCached();
        Container.Bind<TowerBase>().WithId("Opponent").To<Tower>()
            .FromInstance(_fightingUnitsListReference.enemyTowerBase).AsCached();

        Container.Bind<IFightingUnitsList>().To<FightingUnitsList>().FromScriptableObject(_fightingUnitsListReference)
            .AsSingle();

        Container.Bind<IUnitInitialPlacementConfig>().To<UnitInitialPlacementConfig>()
            .FromScriptableObject(_placementConfig).AsSingle();
    }


    private HealthBarViewModel GenerateHealthBar()
    {
        return Instantiate(_healthBarPrefab, _healthBarCanvas);
    }
}