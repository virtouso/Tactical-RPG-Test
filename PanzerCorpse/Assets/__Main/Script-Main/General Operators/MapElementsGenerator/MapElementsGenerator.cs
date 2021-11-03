using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapElementsGenerator : MonoBehaviour,IMapElementsGenerator
{
    [Inject(Id = "Player")] private TowerBase _playerTowerBase;
    [Inject(Id = "Opponent")] private TowerBase _enemyTowerBase;
    [Inject] private HealthBarViewModel.Factory _healthBarFactory;
    [Inject] private IMatchGeneralSettings _matchGeneralSettings;
    [Inject] private IUtilityMatchQueries _matchQueryUtility;
    [Inject] private IGameDataManager _gameDataManager;
    [Inject] private IUnitInitialPlacementConfig _initialPlacementConfig;
    [Inject] private IFightingUnitsList _fightingUnitsList;
    [SerializeField] private Transform _healthBarParent;

    public IEnumerator GeneratePlayerTower()
    {
        yield return new WaitForSeconds(_matchGeneralSettings.ShortWaitTime);
        int xPosition = _matchQueryUtility.MatchModel.Board.GetLength(0) - 1;
        int yPosition = _matchQueryUtility.MatchModel.Board.GetLength(1) / 2;
        FieldCoordinate selectedCoordinate = new FieldCoordinate(xPosition, yPosition);
        Vector3 placedPosition = _matchQueryUtility.MatchModel.Board[xPosition, yPosition].Position;

        TowerBase tower = Instantiate(_playerTowerBase, placedPosition, Quaternion.identity);
        var stats = new TowerCurrentStats(new Model<int>(_matchGeneralSettings.TowersInitialHealth));
        tower.Init(placedPosition, selectedCoordinate, stats);
        _matchQueryUtility.MatchModel.Players[MatchPlayerType.Player].TowerBase = tower;

        AddHealthBarToUnit(stats.Health, tower.transform);
    }

   public IEnumerator GeneratePlayerUnits()
    {
        yield return new WaitForSeconds(_matchGeneralSettings.ShortWaitTime);

        var dataList = _gameDataManager.PlayerData.PlayerProgress.OwnedTanks.Data;
        for (int i = 0; i < dataList.Count; i++)
        {
            int xPosition = _matchQueryUtility.MatchModel.Board.GetLength(0) -
                            _initialPlacementConfig.IndexCoordinates[i].X;
            int yPosition = (_matchQueryUtility.MatchModel.Board.GetLength(1) / 2) +
                            _initialPlacementConfig.IndexCoordinates[i].Y;
            HexPanelBase selectedPanel = _matchQueryUtility.MatchModel.Board[xPosition, yPosition];
            var tankConfig = _fightingUnitsList.FightingUnits[dataList[i].TankId];
            FightingUnitMonoBase tankInstance = Instantiate(tankConfig.GameObject);
            tankInstance.Init(tankConfig.Stats[dataList[i].TankLevel],
                new FieldCoordinate(xPosition, yPosition),
                selectedPanel.Position,
                _fightingUnitsList.PlayerMaterial,
                Vector3.left);
            _matchQueryUtility.MatchModel.Players[MatchPlayerType.Player].FightingUnits.Add(tankInstance);
            AddHealthBarToUnit(tankInstance.CurrentState.HealthAmount, tankInstance.transform);
        }
    }
    
   public IEnumerator GenerateOpponentUnits()
   {
       yield return new WaitForSeconds(_matchGeneralSettings.ShortWaitTime);
       var dataList = _gameDataManager.PlayerData.PlayerProgress.OwnedTanks.Data;
       for (int i = 0; i < dataList.Count; i++)
       {
           int xPosition = 0 + _initialPlacementConfig.IndexCoordinates[i].X;
           int yPosition = (_matchQueryUtility.MatchModel.Board.GetLength(1) / 2) +
                           _initialPlacementConfig.IndexCoordinates[i].Y;
           HexPanelBase selectedPanel = _matchQueryUtility.MatchModel.Board[xPosition, yPosition];
           var tankConfig = _fightingUnitsList.FightingUnits[dataList[i].TankId];
           FightingUnitMonoBase tankInstance = Instantiate(tankConfig.GameObject);
           tankInstance.Init(tankConfig.Stats[dataList[i].TankLevel],
               new FieldCoordinate(xPosition, yPosition),
               selectedPanel.Position,
               _fightingUnitsList.OpponentMaterial,
               Vector3.right);
           _matchQueryUtility.MatchModel.Players[MatchPlayerType.Opponent].FightingUnits.Add(tankInstance);
           AddHealthBarToUnit(tankInstance.CurrentState.HealthAmount, tankInstance.transform);
       }
   }
   
   
  public IEnumerator GenerateOpponentTower()
   {
       yield return new WaitForSeconds(_matchGeneralSettings.ShortWaitTime);
       int xPosition = 0;
       int yPosition = _matchQueryUtility.MatchModel.Board.GetLength(1) / 2;
       FieldCoordinate selectedCoordinate = new FieldCoordinate(xPosition, yPosition);
       Vector3 placedPosition = _matchQueryUtility.MatchModel.Board[xPosition, yPosition].Position;
       TowerBase tower = Instantiate(_enemyTowerBase, placedPosition, Quaternion.identity);
       var stats = new TowerCurrentStats(new Model<int>(_matchGeneralSettings.TowersInitialHealth));
       tower.Init(placedPosition, selectedCoordinate, stats);
       _matchQueryUtility.MatchModel.Players[MatchPlayerType.Opponent].TowerBase = tower;
       AddHealthBarToUnit(stats.Health, tower.transform);
   }
   
    
    private void AddHealthBarToUnit(Model<int> initialHealth, Transform referenceTransform)
    {
        IHealthBarViewModel healthInstance = _healthBarFactory.Create();
        healthInstance.Init(initialHealth, referenceTransform, _healthBarParent);
    }
}
