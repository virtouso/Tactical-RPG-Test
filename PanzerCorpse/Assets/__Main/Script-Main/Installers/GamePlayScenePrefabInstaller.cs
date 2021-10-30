using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GamePlayScenePrefabInstaller : MonoInstaller
{
    [SerializeField] private FightingUnitsList _fightingUnitsListReference;
    [SerializeField] private HexPanel _hexPanelPrefab;
    [SerializeField] private HealthBarViewModel _healthBarPrefab;


    public override void InstallBindings()
    {
        Container.Bind<HexPanelBase>().To<HexPanel>().FromInstance(_hexPanelPrefab).AsSingle();
        Container.Bind<IHealthBarViewModel>().To<HealthBarViewModel>().FromInstance(_healthBarPrefab).AsSingle();


        Container.Bind<TowerBase>().WithId("Player").To<Tower>()
            .FromInstance(_fightingUnitsListReference.playerTowerBase).AsCached();
        Container.Bind<TowerBase>().WithId("Opponent").To<Tower>()
            .FromInstance(_fightingUnitsListReference.enemyTowerBase).AsCached();

        Container.Bind<FightingUnitConfigBase>().WithId(FightingUnitNamesEnum.MouseTank).To<MouseTankConfig>()
            .FromScriptableObject(_fightingUnitsListReference.FightingUnits[FightingUnitNamesEnum.MouseTank])
            .AsSingle();

        Container.Bind<FightingUnitConfigBase>().WithId(FightingUnitNamesEnum.TigerTank).To<TigerTankConfig>()
            .FromScriptableObject(_fightingUnitsListReference.FightingUnits[FightingUnitNamesEnum.TigerTank])
            .AsSingle();
    }
}