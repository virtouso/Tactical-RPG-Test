using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Codice.Client.BaseCommands;
using ModestTree;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class GameStateManager : MonoBehaviour, IGameStateManager
{
    public bool PlayerCanPlay => _matchQueryUtility.MatchModel.Turn.Data == MatchPlayerType.Player && !_updatingUi;

    public System.Action<MatchPlayerType> OnTurnUpdate { get; set; }
    public System.Action<bool, MatchPlayerType> ActionFinished { get; set; }
    public System.Action<MatchPlayerType> OnGameFinished { get; set; }
    [Inject] private IMapElementsGenerator _mapElementsGenerator;
    [Inject] private IUtilityMatchQueries _matchQueryUtility;
    [Inject] private IGameDataManager _gameDataManager;
    [Inject] private IHexMapGenerator _hexMapGenerator;
    [Inject] private IGamePlayCamera _gamePlayCamera;
    [Inject] private IUtilityMatchGeneral _matchGeneralUtility;
    [Inject] private HealthBarViewModel.Factory _healthBarFactory;
    [Inject] private IMatchGeneralSettings _matchGeneralSettings;

    [SerializeField] private Transform _healthBarParent;


    private bool _updatingUi = true;

    #region Main Utility

    private void StartMatch()
    {
        StartCoroutine(PlayCameraStartAnimation());
        _updatingUi = true;
        Observable.FromCoroutine(LoadThemeScene)
            .SelectMany(GenerateField)
            .SelectMany(GeneratePlayerTower)
            .SelectMany(GenerateOpponentTower)
            .SelectMany(GeneratePlayerUnits)
            .SelectMany(GenerateOpponentUnits)
            .SelectMany(UpdateTurn(true))
            .SelectMany((result) =>
            {
                _updatingUi = false;
                return Observable.Return(_updatingUi);
            }).SelectMany((result) =>
            {
                var test = _matchQueryUtility.MatchModel;
                return Observable.Return(true);
            })
            .Subscribe();
    }

    #endregion


    #region Sub Utility

    private IEnumerator LoadThemeScene()
    {
        SceneManager.LoadScene(_gameDataManager.GetPlayerSelectedMap(), LoadSceneMode.Additive);
        yield return new WaitForSeconds(_matchGeneralSettings.AverageWaitTime);
    }


    private IEnumerator GenerateField()
    {
        yield return StartCoroutine(_hexMapGenerator.GenerateHexGrid());
        _matchQueryUtility.MatchModel.Board = _hexMapGenerator.HexGrid;
    }


    private IEnumerator GeneratePlayerTower()
    {
        yield return StartCoroutine(_mapElementsGenerator.GeneratePlayerTower());
    }

    private IEnumerator PlayCameraStartAnimation()
    {
        yield return StartCoroutine(_gamePlayCamera.PlayStartAnimation());
    }

    private IEnumerator GenerateOpponentTower()
    {
        yield return StartCoroutine(_mapElementsGenerator.GenerateOpponentTower());
    }

    private IEnumerator GeneratePlayerUnits()
    {
        yield return _mapElementsGenerator.GeneratePlayerUnits();
    }

    private IEnumerator GenerateOpponentUnits()
    {
        yield return StartCoroutine(_mapElementsGenerator.GenerateOpponentUnits());
    }

    private void AddHealthBarToUnit(Model<int> initialHealth, Transform referenceTransform)
    {
        IHealthBarViewModel healthInstance = _healthBarFactory.Create();
        healthInstance.Init(initialHealth, referenceTransform, _healthBarParent);
    }

    private void ClearBoardColors()
    {
        foreach (var item in _matchQueryUtility.MatchModel.Board)
        {
            item.UpdateMaterial(_matchGeneralSettings.NormalMaterial);
        }
    }

    private void ColorizeValidMoves(List<ActionQuery> validActions)
    {
        foreach (var item in validActions)
        {
            if (item.ActionType == ActionType.Shoot)
            {
                var currentHexPanel = _matchQueryUtility.MatchModel.Board[item.Goal.X, item.Goal.Y];
                currentHexPanel.UpdateMaterial(_matchGeneralSettings.AttackMaterial);
            }
            else if (item.ActionType == ActionType.Move)
            {
                var currentHexPanel = _matchQueryUtility.MatchModel.Board[item.Goal.X, item.Goal.Y];
                currentHexPanel.UpdateMaterial(_matchGeneralSettings.MoveMaterial);
            }
        }
    }


    private IEnumerator UpdateTurn(bool isInitial)
    {
        MatchPlayerType currentTurn = MatchPlayerType.None;
        if (isInitial)
        {
            _matchQueryUtility.MatchModel.Turn.Data = (MatchPlayerType)UnityEngine.Random.Range(0, 1);

            currentTurn = _matchQueryUtility.MatchModel.Turn.Data;
            _matchQueryUtility.MatchModel.Players[currentTurn].CurrentPermittedMoves =
                _matchQueryUtility.MatchModel.NumberOfPermittedMovesInOneTurn;

            OnTurnUpdate?.Invoke(_matchQueryUtility.MatchModel.Turn.Data);
        }
        else
        {
            currentTurn = _matchQueryUtility.MatchModel.Turn.Data;

            _matchQueryUtility.MatchModel.Players[currentTurn].CurrentPermittedMoves--;

            if (_matchQueryUtility.MatchModel.Players[currentTurn].CurrentPermittedMoves <= 0)
            {
                _matchQueryUtility.MatchModel.Turn.Data = _matchGeneralUtility.SwitchPlayers(currentTurn);

                _matchQueryUtility.MatchModel.Players[_matchQueryUtility.MatchModel.Turn.Data].CurrentPermittedMoves =
                    _matchQueryUtility.MatchModel.NumberOfPermittedMovesInOneTurn;
            }

            OnTurnUpdate?.Invoke(_matchQueryUtility.MatchModel.Turn.Data);
        }

        yield return new WaitForSeconds(_matchGeneralSettings.ShortWaitTime);
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
        var validMoves = _matchQueryUtility.ListOfLegitMovesForCoordinate(coordinate);
        if (validMoves == null)
        {
            ActionFinished?.Invoke(false, MatchPlayerType.Player);
            ClearBoardColors();
            return;
        }

        ColorizeValidMoves(validMoves);
    }


    public void SelectedWholeMoveByPlayers(ActionQuery actionQuery)
    {
        bool moveIsValid = _matchQueryUtility.CheckActionIsValid(actionQuery);
        Debug.Log("Action Is valid::" + moveIsValid + ",from:" + actionQuery.From);
        ClearBoardColors();
        if (!moveIsValid)
        {
            ActionFinished?.Invoke(false, actionQuery.From);
            return;
        }

        _updatingUi = true;
        ApplyMoveCoroutine(actionQuery).SelectMany((query) => Observable.FromCoroutine(EmptyWait))
            .SelectMany(RemoveDeadUnits()).SelectMany(CheckMatchIsFinished()).SelectMany((winner) =>
            {
                if (winner == MatchPlayerType.None)
                    StartCoroutine(UpdateTurn(false));
                else
                    OnGameFinished?.Invoke(winner);
                
                return Observable.Return(true);
            }).SelectMany((result) =>
            {
                _updatingUi = false;
                return Observable.Return(_updatingUi);
            }).Subscribe();
    }

    private IObservable<ActionQuery> ApplyMoveCoroutine(ActionQuery action)
    {
        _matchQueryUtility.ApplyMove(action);
        return Observable.Return(action);
    }

    private IObservable<bool> RemoveDeadUnits()
    {
        _matchQueryUtility.RemoveDeadUnityFromList();
        return Observable.Return(true);
    }

    private IObservable<MatchPlayerType> CheckMatchIsFinished()
    {
        MatchPlayerType result = _matchQueryUtility.CheckMatchIsFinished();
        return Observable.Return(result);
    }

    //because uniRx sequential delayed action work with coroutines
    private IEnumerator EmptyWait()
    {
        yield return new WaitForSeconds(_matchGeneralSettings.LongWaitTime);
    }

    #endregion


    #region Unity Callbacks

    private void Start()
    {
        StartMatch();
    }

    #endregion
}