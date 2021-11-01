using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using ModestTree;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class GameStateManager : MonoBehaviour, IGameStateManager
{
    public bool PlayerCanPlay => _matchQueryUtility.MatchModel.Turn.Data == MatchPlayerType.Player;
    public System.Action<MatchPlayerType> OnTurnUpdate { get; set; }

    [Inject] private IUtilityMatchQueries _matchQueryUtility;
    [Inject] private IFightingUnitsList _fightingUnitsList;
    [Inject] private IGameDataManager _gameDataManager;
    [Inject] private IHexMapGenerator _hexMapGenerator;
    [Inject] private IGamePlayCamera _gamePlayCamera;
    [Inject] private IUtilityMatchQueries _matchState;
    [Inject] private IUnitInitialPlacementConfig _initialPlacementConfig;
    [Inject] private HealthBarViewModel.Factory _healthBarFactory;
    [Inject(Id = "Player")] private TowerBase _playerTowerBase;
    [Inject(Id = "Opponent")] private TowerBase _enemyTowerBase;
   [SerializeField] private Transform _healthBarParent;

    #region Main Utility

    private void StartMatch()
    {
        StartCoroutine(PlayCameraStartAnimation());

        Observable.FromCoroutine(LoadThemeScene)
            .SelectMany(GenerateField)
            .SelectMany(GeneratePlayerTower)
            .SelectMany(GenerateOpponentTower)
            .SelectMany(GeneratePlayerUnits)
            .SelectMany(GenerateOpponentUnits)
            .SelectMany(UpdateTurn(true))
            .Subscribe();
    }

    #endregion


    #region Sub Utility

    private IEnumerator LoadThemeScene()
    {
        SceneManager.LoadScene(_gameDataManager.GetPlayerSelectedMap(), LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
    }


    private IEnumerator GenerateField()
    {
        yield return StartCoroutine(_hexMapGenerator.GenerateHexGrid());
        _matchState.MatchModel.Board = _hexMapGenerator.HexGrid;
    }


    private IEnumerator GeneratePlayerTower()
    {
        yield return new WaitForSeconds(0.1f);
        int xPosition = _matchState.MatchModel.Board.GetLength(0) - 1;
        int yPosition = _matchState.MatchModel.Board.GetLength(1) / 2;
        FieldCoordinate selectedCoordinate = new FieldCoordinate(xPosition, yPosition);
        Vector3 placedPosition = _matchState.MatchModel.Board[xPosition, yPosition].Position;

        TowerBase tower = Instantiate(_playerTowerBase, placedPosition, Quaternion.identity);
        var stats = new TowerCurrentStats(new Model<int>(100));
        tower.Init(placedPosition, selectedCoordinate, stats);
        _matchState.MatchModel.Players[MatchPlayerType.Player].TowerBase = tower;

        AddHealthBarToUnit(stats.Health, tower.transform);
    }

    private IEnumerator PlayCameraStartAnimation()
    {
        yield return StartCoroutine(_gamePlayCamera.PlayStartAnimation());
    }

    private IEnumerator GenerateOpponentTower()
    {
        yield return new WaitForSeconds(0.1f);
        int xPosition = 0;
        int yPosition = _matchState.MatchModel.Board.GetLength(1) / 2;
        FieldCoordinate selectedCoordinate = new FieldCoordinate(xPosition, yPosition);
        Vector3 placedPosition = _matchState.MatchModel.Board[xPosition, yPosition].Position;
        TowerBase tower = Instantiate(_enemyTowerBase, placedPosition, Quaternion.identity);
        var stats = new TowerCurrentStats(new Model<int>(100));
        tower.Init(placedPosition, selectedCoordinate, stats);
        _matchState.MatchModel.Players[MatchPlayerType.Player].TowerBase = tower;
        AddHealthBarToUnit(stats.Health, tower.transform);
    }

    private IEnumerator GeneratePlayerUnits()
    {
        yield return new WaitForSeconds(0.1f);

        var dataList = _gameDataManager.PlayerData.PlayerProgress.OwnedTanks.Data;
        for (int i = 0; i < dataList.Count; i++)
        {
            int xPosition = _matchState.MatchModel.Board.GetLength(0) - _initialPlacementConfig.IndexCoordinates[i].X;
            int yPosition = (_matchState.MatchModel.Board.GetLength(1) / 2) +
                            _initialPlacementConfig.IndexCoordinates[i].Y;
            HexPanelBase selectedPanel = _matchQueryUtility.MatchModel.Board[xPosition, yPosition];
            var tankConfig = _fightingUnitsList.FightingUnits[dataList[i].TankId];
            FightingUnitMonoBase tankInstance = Instantiate(tankConfig.GameObject);
            tankInstance.Init(tankConfig.Stats[dataList[i].TankLevel],
                new FieldCoordinate(xPosition, yPosition),
                selectedPanel.Position,
                _fightingUnitsList.PlayerMaterial,
                Vector3.left);
            _matchState.MatchModel.Players[MatchPlayerType.Player].FightingUnits.Add(tankInstance);
            AddHealthBarToUnit(tankInstance.CurrentState.HealthAmount ,tankInstance.transform);
        }
    }

    private IEnumerator GenerateOpponentUnits()
    {
        yield return new WaitForSeconds(0.1f);
        var dataList = _gameDataManager.PlayerData.PlayerProgress.OwnedTanks.Data;
        for (int i = 0; i < dataList.Count; i++)
        {
            int xPosition = 0 + _initialPlacementConfig.IndexCoordinates[i].X;
            int yPosition = (_matchState.MatchModel.Board.GetLength(1) / 2) +
                            _initialPlacementConfig.IndexCoordinates[i].Y;
            HexPanelBase selectedPanel = _matchQueryUtility.MatchModel.Board[xPosition, yPosition];
            var tankConfig = _fightingUnitsList.FightingUnits[dataList[i].TankId];
            FightingUnitMonoBase tankInstance = Instantiate(tankConfig.GameObject);
            tankInstance.Init(tankConfig.Stats[dataList[i].TankLevel],
                new FieldCoordinate(xPosition, yPosition),
                selectedPanel.Position,
                _fightingUnitsList.OpponentMaterial,
                Vector3.right);
            _matchState.MatchModel.Players[MatchPlayerType.Opponent].FightingUnits.Add(tankInstance);
            AddHealthBarToUnit(tankInstance.CurrentState.HealthAmount ,tankInstance.transform);
        }
    }

    private void AddHealthBarToUnit(Model<int> initialHealth, Transform referenceTransform)
    {
       IHealthBarViewModel healthInstance = _healthBarFactory.Create();
        healthInstance.Init(initialHealth, referenceTransform,_healthBarParent);
        
    }

    private void ClearBoardColors()
    {
        throw new NotImplementedException();
    }


    private IEnumerator UpdateTurn(bool isInitial)
    {
        MatchPlayerType currentTurn = MatchPlayerType.None;
        if (isInitial)
        {
            _matchQueryUtility.MatchModel.Turn.Data =(MatchPlayerType) UnityEngine.Random.Range(0, 1);
            
           currentTurn = _matchQueryUtility.MatchModel.Turn.Data;
            _matchQueryUtility.MatchModel.Players[currentTurn].CurrentPermittedMoves =
                _matchQueryUtility.MatchModel.NumberOfPermittedMovesInOneTurn;
            
            OnTurnUpdate?.Invoke( _matchQueryUtility.MatchModel.Turn.Data);
        }
        else
        {
          currentTurn = _matchQueryUtility.MatchModel.Turn.Data;
            
            _matchQueryUtility.MatchModel.Players[currentTurn].CurrentPermittedMoves--;

            if (_matchQueryUtility.MatchModel.Players[currentTurn].CurrentPermittedMoves <= 0)
            {
                _matchQueryUtility.MatchModel.Turn.Data = ~ _matchQueryUtility.MatchModel.Turn.Data;

                _matchQueryUtility.MatchModel.Players[currentTurn].CurrentPermittedMoves =
                    _matchQueryUtility.MatchModel.NumberOfPermittedMovesInOneTurn;
                OnTurnUpdate?.Invoke( _matchQueryUtility.MatchModel.Turn.Data);

            }
            
            
        }

        yield return new WaitForSeconds(0.1f);
    }
    
    #endregion

    #region InputHandlers

    // i know its better to place  public functions on top of class
    // but as public stuf are included in interface  and these are managed in region, its ok!
    public void PlayerCleared()
    {
        ClearBoardColors();
    }

    public void PlayerSelectedOrigin(FieldCoordinate coordinate)
    {
    }


    public void SelectedWholeMoveByPlayers(ActionQuery actionQuery)
    {
        
    }

   
    
    #endregion


    #region Unity Callbacks

    private void Start()
    {
        StartMatch();
    }

    #endregion
}