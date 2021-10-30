using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStateManager : MonoBehaviour, IGameStateManager
{
//    [Inject] private IUtilityMatchGeneral _generalMatchUtility;
    [Inject] private IUtilityMatchQueries _matchQueryUtility;
    [Inject] private IFightingUnitsList _fightingUnitsList;
    [Inject] private IGameDataManager _gameDataManager;
    [Inject] private IHexMapGenerator _hexMapGenerator;
    [Inject] private IGamePlayCamera _gamePlayCamera;
    [Inject] private IUtilityMatchQueries _matchState;
    [Inject] private IUnitInitialPlacementConfig _initialPlacementConfig;

    [Inject(Id = "Player")] private TowerBase _playerTowerBase;
    [Inject(Id = "Opponent")] private TowerBase _enemyTowerBase;

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
        Vector3 placedPosition = _matchState.MatchModel.Board[xPosition, yPosition].Position;
        _matchState.MatchModel.Players[MatchPlayerType.Player].TowerBase =
            Instantiate(_playerTowerBase, placedPosition, Quaternion.identity);
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
        Vector3 placedPosition = _matchState.MatchModel.Board[xPosition, yPosition].Position;
        _matchState.MatchModel.Players[MatchPlayerType.Player].TowerBase =
            Instantiate(_enemyTowerBase, placedPosition, Quaternion.identity);
    }

    private IEnumerator GeneratePlayerUnits()
    {
        yield return new WaitForSeconds(0.1f);

        var dataList = _gameDataManager.PlayerData.PlayerProgress.OwnedTanks.Data;
        for (int i = 0; i < dataList.Count; i++)
        {
            int xPosition= _matchState.MatchModel.Board.GetLength(0) - _initialPlacementConfig.IndexCoordinates[i].X;
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
        }
    }

    private IEnumerator GenerateOpponentUnits()
    {
        yield return new WaitForSeconds(0.1f);

        var dataList = _gameDataManager.PlayerData.PlayerProgress.OwnedTanks.Data;
        for (int i = 0; i < dataList.Count; i++)
        {
            int xPosition = 0 + _initialPlacementConfig.IndexCoordinates[i].X;
            int yPosition = (_matchState.MatchModel.Board.GetLength(1) / 2) + _initialPlacementConfig.IndexCoordinates[i].Y;
            HexPanelBase selectedPanel = _matchQueryUtility.MatchModel.Board[xPosition, yPosition];
            var tankConfig = _fightingUnitsList.FightingUnits[dataList[i].TankId];
            FightingUnitMonoBase tankInstance = Instantiate(tankConfig.GameObject);
            tankInstance.Init(tankConfig.Stats[dataList[i].TankLevel],
                new FieldCoordinate(xPosition, yPosition),
                selectedPanel.Position,
                _fightingUnitsList.OpponentMaterial,
                Vector3.right);
            _matchState.MatchModel.Players[MatchPlayerType.Opponent].FightingUnits.Add(tankInstance);
        }
    }

    #endregion


    #region Unity Callbacks

    private void Start()
    {
        StartMatch();
    }

    #endregion
}