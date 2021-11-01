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


    public void RemoveDeadUnityFromList()
    {
        foreach (var playerUnit in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (playerUnit.CurrentState.HealthAmount.Data <= 0)
                _matchModel.Players[MatchPlayerType.Player].FightingUnits.Remove(playerUnit);
        }

        foreach (var opponentUnit in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            if (opponentUnit.CurrentState.HealthAmount.Data <= 0)
                _matchModel.Players[MatchPlayerType.Opponent].FightingUnits.Remove(opponentUnit);
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
        throw new NotImplementedException();
    }


    public List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate)
    {
        FightingUnitMonoBase selectedPlayerUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, coordinate))
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
                if (_utilityMatchGeneral.Check2CoordinatesAreEqual(friendlyUnit.FieldCoordinate.Data,
                    hexPanel.FieldCoordinate))
                    thereIsFriendlyInHex = true;
            }

            if (thereIsFriendlyInHex)
                continue;

            // check there is enemy unit or tower in that hex or not.
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(
                _matchModel.Players[MatchPlayerType.Opponent].TowerBase.FieldCoordinate, hexPanel.FieldCoordinate))
                possibleQueries.Add(new ActionQuery(ActionType.Shoot, coordinate, hexPanel.FieldCoordinate,MatchPlayerType.None));

            foreach (var enemyUnit in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
            {
                if (_utilityMatchGeneral.Check2CoordinatesAreEqual(enemyUnit.FieldCoordinate.Data,
                    hexPanel.FieldCoordinate))
                    possibleQueries.Add(new ActionQuery(ActionType.Shoot, coordinate, enemyUnit.FieldCoordinate.Data,MatchPlayerType.None));
            }

            possibleQueries.Add(new ActionQuery(ActionType.Move, coordinate, hexPanel.FieldCoordinate,MatchPlayerType.None));
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
        foreach (var friendlyUnit in _matchModel.Players[actionQuery.From ].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(actionQuery.Current, friendlyUnit.FieldCoordinate.Data))
                selectedFriendlyUnit = friendlyUnit;
        }

        FightingUnitMonoBase selectedEnemyUnit = null;
        foreach (var enemyUnit in _matchModel.Players[~actionQuery.From].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(actionQuery.Current, enemyUnit.FieldCoordinate.Data))
                selectedEnemyUnit = enemyUnit;
        }
        
        switch (actionQuery.ActionType)
        {
            case ActionType.Move:
                selectedFriendlyUnit.FieldCoordinate.Data = actionQuery.Goal;
                break;
            case ActionType.Shoot:
                if (selectedEnemyUnit == null)
                {
                    _matchModel.Players[~actionQuery.From].TowerBase.TowerCurrentStats.Health.Data-= 
                      selectedFriendlyUnit.CurrentState.DamageAmount.Data;
                }
                else
                {
                    selectedEnemyUnit.CurrentState.HealthAmount.Data -=
                        selectedFriendlyUnit.CurrentState.DamageAmount.Data;
                }

                break;
        }
    }

    public bool CheckCoordinateIsInsideBoard(FieldCoordinate coordinate)
    {
        bool insideX = coordinate.X >= 0 && coordinate.X < _matchModel.Board.GetLength(0);
        bool insideY = coordinate.Y >= 0 && coordinate.Y < _matchModel.Board.GetLength(1);

        return insideX && insideY;
    }

    public bool CheckHexPanelIsMasked(FieldCoordinate coordinate)
    {
        if (_utilityMatchGeneral.Check2CoordinatesAreEqual(coordinate,
            _matchModel.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate)) return true;
        
        if (_utilityMatchGeneral.Check2CoordinatesAreEqual(coordinate,
            _matchModel.Players[MatchPlayerType.Opponent].TowerBase.FieldCoordinate)) return true;

        foreach (var item in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, coordinate))
                return true;
        }

        foreach (var item in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, coordinate))
                return true;
        }
        
        
        return false;
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
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, actionQuery.Current))
                selectedPlayerUnit = item;
        }

        if (selectedPlayerUnit == null)
            return false;

        FightingUnitMonoBase selectedOpponentUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, actionQuery.Goal))
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
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, actionQuery.Current))
                selectedPlayerUnit = item;
        }

        if (selectedPlayerUnit == null)
            return false;

        if (_matchModel.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate == actionQuery.Goal)
            return false;

        foreach (var item in _matchModel.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, actionQuery.Goal))
                return false;
        }
        
        FightingUnitMonoBase selectedOpponentUnit = null;
        foreach (var item in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, actionQuery.Goal))
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