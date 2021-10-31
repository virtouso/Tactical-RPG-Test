using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


// certainly there are better searches with better time-complexity like searching in units instead of
// searching the board but most of standard turn-based query libraries do like this.
// match should query just he board not units or moving pieces.

//also could make shorter functions to extract them but for a tested library it can be just good.

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

        //prefer to fail fast instead of ignore bugs
        throw new UnityException();
    }


    public List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate)
    {
        FightingUnitMonoBase selectedPlayerUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (item.FieldCoordinate == coordinate)
                selectedPlayerUnit = item;
        }

        if (selectedPlayerUnit == null)
            return null;


        List<ActionQuery> possibleQueries = new List<ActionQuery>();
        foreach (var hexPanel in _matchModel.Board)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(hexPanel.FieldCoordinate, coordinate))
                continue;
            if (_utilityMatchGeneral.CalculateDistanceBetween2Coordinates(coordinate, hexPanel.FieldCoordinate) >
                selectedPlayerUnit.InitialStats.MovingUnitsInTurn)
                continue;
            //check no friendly unit or tower in that hex

            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(hexPanel.FieldCoordinate,
                _matchModel.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate))
                continue;
            bool thereIsFriendlyInHex = false;
            foreach (var friendlyUnit in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
            {
                if (_utilityMatchGeneral.Check2CoordinatesAreEqual(friendlyUnit.FieldCoordinate,
                    hexPanel.FieldCoordinate))
                    thereIsFriendlyInHex = true;
            }

            if (thereIsFriendlyInHex)
                continue;

            // check there is enemy unit or tower in that hex or not.
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(
                _matchModel.Players[MatchPlayerType.Opponent].TowerBase.FieldCoordinate, hexPanel.FieldCoordinate))
                possibleQueries.Add(new ActionQuery(ActionType.Shoot, coordinate, hexPanel.FieldCoordinate));

            foreach (var enemyUnit in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
            {
                if (_utilityMatchGeneral.Check2CoordinatesAreEqual(enemyUnit.FieldCoordinate, hexPanel.FieldCoordinate))
                    possibleQueries.Add(new ActionQuery(ActionType.Shoot, coordinate, enemyUnit.FieldCoordinate));
            }

            possibleQueries.Add(new ActionQuery(ActionType.Move, coordinate, hexPanel.FieldCoordinate));
        }


        return possibleQueries;
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

    public void ApplyMove(ActionQuery actionQuery)
    {
        FightingUnitMonoBase selectedFriendlyUnit = null;
        foreach (var friendlyUnit in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (actionQuery.Current == friendlyUnit.FieldCoordinate)
                selectedFriendlyUnit = friendlyUnit;
        }
        
        switch (actionQuery.ActionType)
        {
            case ActionType.Move:
                selectedFriendlyUnit.FieldCoordinate = actionQuery.Goal;
                break;
            case ActionType.Shoot:
                break;
        }
    }

    public void UpdateTurnState()
    {
        _matchModel.Players[_matchModel.Turn.Data].CurrentPermittedMoves--;

        if (_matchModel.Players[_matchModel.Turn.Data].CurrentPermittedMoves <= 0)
        {
            _matchModel.Turn.Data = ~_matchModel.Turn.Data;
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

    #endregion
}