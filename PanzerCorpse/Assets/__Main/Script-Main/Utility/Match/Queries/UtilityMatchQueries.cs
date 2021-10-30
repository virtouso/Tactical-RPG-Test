using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;
using Zenject;

public class UtilityMatchQueries : IUtilityMatchQueries
{
    [Inject] private IUtilityMatchGeneral _utilityMatchGeneral;
    
    private MatchModel _matchModel;

    public MatchModel MatchModel
    {
        get
        {
            if (_matchModel == null)
                _matchModel = new MatchModel();
            return _matchModel;
        }
        
        
    }

    public MatchModel InitGameStateFields(MatchModel matchModel)
    {
        // game initial data is filled in multi steps. no need for this
        throw new System.NotImplementedException();
    }

    public FieldCoordinate GetTowerPosition(MatchPlayerType matchPlayerType)
    {
        return _matchModel.Players[matchPlayerType].TowerBase.FieldCoordinate;
    }

   

    public bool CheckActionIsValid(ActionQuery actionQuery)
    {
        switch (actionQuery.ActionType)
        {
            case ActionType.Move:
                return CheckMoveActionIsValid(actionQuery);

                break;
            case ActionType.Shoot:

                return CheckAttackActionIsValid(actionQuery);
                break;
        }

        return false;
    }

    private bool CheckMoveActionIsValid(ActionQuery actionQuery)
    {
        FightingUnitMonoBase selectedPlayerUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (item.FieldCoordinate == actionQuery.Current)
                selectedPlayerUnit = item;
        }

        if (selectedPlayerUnit == null)
            return false;


        FightingUnitMonoBase selectedOpponentUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            if (item.FieldCoordinate == actionQuery.Goal)
                selectedOpponentUnit = item;
        }

        if (actionQuery.Goal == _matchModel.Players[MatchPlayerType.Opponent].TowerBase.FieldCoordinate)
            return false;

        if (selectedOpponentUnit == null)
            return true;

        if (_utilityMatchGeneral.CalculateDistanceBetween2Coordinates(actionQuery.Current, actionQuery.Goal) >
            selectedPlayerUnit.InitialStats.MovingUnitsInTurn)
            return false;

        return true;
    }


    public List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate)
    {
        throw new System.NotImplementedException();
    }

    public MatchPlayerType CheckMatchIsFinished()
    {
        if (_matchModel.Players[MatchPlayerType.Player].TowerBase.TowerCurrentStats.Health.Data <= 0)
            return MatchPlayerType.Opponent;
        if (_matchModel.Players[MatchPlayerType.Opponent].TowerBase.TowerCurrentStats.Health.Data <= 0)
            return MatchPlayerType.Player;

        bool playerAllUnitsDead = true;
        foreach (var unit in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (unit.CurrentState.HealthAmount.Data > 0)
                playerAllUnitsDead = false;
        }

        if (playerAllUnitsDead)
            return MatchPlayerType.Opponent;
        
        bool opponentAllUnitsDead = true;
        foreach (var unit in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            if (unit.CurrentState.HealthAmount.Data > 0)
                opponentAllUnitsDead = false;
        }
        
        if (opponentAllUnitsDead)
            return MatchPlayerType.Player;
        

        return MatchPlayerType.None;
    }

    public MatchModel ApplyMove(ActionQuery actionQuery)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateTurnState()
    {
        _matchModel.Players[_matchModel.Turn.Data].CurrentPermittedMoves--;

        if (_matchModel.Players[_matchModel.Turn.Data].CurrentPermittedMoves <= 0)
        {
            _matchModel.Turn.Data =  ~_matchModel.Turn.Data;
            _matchModel.Players[_matchModel.Turn.Data].CurrentPermittedMoves =
                _matchModel.NumberOfPermittedMovesInOneTurn;
        }
        
    }



    #region Sub Utility

    private bool CheckAttackActionIsValid(ActionQuery actionQuery)
    {
        FightingUnitMonoBase selectedPlayerUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (item.FieldCoordinate == actionQuery.Current)
                selectedPlayerUnit = item;
        }

        if (selectedPlayerUnit == null)
            return false;

        FightingUnitMonoBase selectedOpponentUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            if (item.FieldCoordinate == actionQuery.Goal)
                selectedOpponentUnit = item;
        }

        if (actionQuery.Goal == _matchModel.Players[MatchPlayerType.Opponent].TowerBase.FieldCoordinate)
            return true;

        if (selectedOpponentUnit == null)
            return false;

        return true;
    }

    #endregion
   
}