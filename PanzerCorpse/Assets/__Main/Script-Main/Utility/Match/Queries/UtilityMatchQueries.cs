using System;
using System.Collections.Generic;
using System.Linq;
using Panzers.Entities;
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
        foreach (var playerUnit in _matchModel.Players[MatchPlayerType.Player].FightingUnits.ToList())
        {
            if (playerUnit.CurrentState.HealthAmount.Data <= 0)
                _matchModel.Players[MatchPlayerType.Player].FightingUnits.Remove(playerUnit);
        }

        foreach (var opponentUnit in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits.ToList())
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
        bool sameCurrentAndDestination =
            _utilityMatchGeneral.Check2CoordinatesAreEqual(actionQuery.Current, actionQuery.Goal);
        if (sameCurrentAndDestination)
            return false;

        bool currentInsideBoard = CheckCoordinateIsInsideBoard(actionQuery.Current);
        if (!currentInsideBoard) return false;

        bool destinationInsideBoard = CheckCoordinateIsInsideBoard(actionQuery.Goal);
        if (!destinationInsideBoard) return false;

        FightingUnitMonoBase selectedUnit = null;
        foreach (var unit in _matchModel.Players[actionQuery.From].FightingUnits)
        {
            bool thereIsUnit =
                _utilityMatchGeneral.Check2CoordinatesAreEqual(unit.FieldCoordinate.Data, actionQuery.Current);
            if (thereIsUnit)
                selectedUnit = unit;
        }

        bool currentIsPlayerUnit = selectedUnit != null;
        if (!currentIsPlayerUnit)
            return false;


        FightingUnitMonoBase goalUnit = null;
        foreach (var unit in _matchModel.Players[actionQuery.From].FightingUnits)
        {
            bool thereIsUnit =
                _utilityMatchGeneral.Check2CoordinatesAreEqual(unit.FieldCoordinate.Data, actionQuery.Goal);
            if (thereIsUnit)
                goalUnit = unit;
        }

        bool goalIsPlayerUnit = goalUnit != null;
        if (goalIsPlayerUnit)
            return false;

        bool goalIsPlayerTower = _utilityMatchGeneral.Check2CoordinatesAreEqual(actionQuery.Goal,
            _matchModel.Players[actionQuery.From].TowerBase.FieldCoordinate);

        if (goalIsPlayerTower)
            return false;

        int distance =
            _utilityMatchGeneral.CalculateDistanceBetween2Coordinates(actionQuery.Current, actionQuery.Goal);
        bool inRange = distance <= selectedUnit.CurrentState.MovingUnitsInTurn.Data;
        if (!inRange)
            return false;

        return true;
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
            if (_utilityMatchGeneral.CalculateDistanceBetween2Coordinates(coordinate,
                    hexPanel.FieldCoordinate) >
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
                _matchModel.Players[MatchPlayerType.Opponent].TowerBase.FieldCoordinate,
                hexPanel.FieldCoordinate))
            {
                possibleQueries.Add(new ActionQuery(ActionType.Shoot, coordinate, hexPanel.FieldCoordinate,
                    MatchPlayerType.Player));
                continue;
            }

            bool addedEnemy = false;
            foreach (var enemyUnit in _matchModel.Players[MatchPlayerType.Opponent].FightingUnits)
            {
                if (addedEnemy) continue;
                if (_utilityMatchGeneral.Check2CoordinatesAreEqual(enemyUnit.FieldCoordinate.Data,
                    hexPanel.FieldCoordinate))
                {
                    possibleQueries.Add(new ActionQuery(ActionType.Shoot, coordinate,
                        enemyUnit.FieldCoordinate.Data,
                        MatchPlayerType.Player));
                    addedEnemy = true;
                }
            }

            if (addedEnemy)
                continue;

            possibleQueries.Add(new ActionQuery(ActionType.Move, coordinate, hexPanel.FieldCoordinate,
                MatchPlayerType.None));
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
        foreach (var friendlyUnit in _matchModel.Players[actionQuery.From].FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(actionQuery.Current, friendlyUnit.FieldCoordinate.Data))
                selectedFriendlyUnit = friendlyUnit;
        }

        FightingUnitMonoBase selectedEnemyUnit = null;
        foreach (var enemyUnit in _matchModel.Players[_utilityMatchGeneral.SwitchPlayers(actionQuery.From)]
            .FightingUnits)
        {
            if (_utilityMatchGeneral.Check2CoordinatesAreEqual(actionQuery.Goal,
                enemyUnit.FieldCoordinate.Data))
                selectedEnemyUnit = enemyUnit;
        }


        var enemyTower = _matchModel.Players[_utilityMatchGeneral.SwitchPlayers(actionQuery.From)].TowerBase;
        TowerBase enemySelectedTower = null;
        if (_utilityMatchGeneral.Check2CoordinatesAreEqual(actionQuery.Goal, enemyTower.FieldCoordinate))
            enemySelectedTower = enemyTower;

        ActionType action = ActionType.None;
        if (selectedEnemyUnit == null && enemySelectedTower == null)
        {
            action = ActionType.Move;
        }
        else
        {
            action = ActionType.Shoot;
        }

        switch (action)
        {
            case ActionType.Move:
                selectedFriendlyUnit.FieldCoordinate.Data = actionQuery.Goal;
                break;
            case ActionType.Shoot:
                if (selectedEnemyUnit == null)
                {
                    enemySelectedTower.TowerCurrentStats.Health.Data -=
                        selectedFriendlyUnit.CurrentState.DamageAmount.Data;
                    selectedFriendlyUnit.OnAttack(enemySelectedTower.transform.position);
                }
                else
                {
                    selectedEnemyUnit.CurrentState.HealthAmount.Data -=
                        selectedFriendlyUnit.CurrentState.DamageAmount.Data;
                    selectedFriendlyUnit.OnAttack(selectedEnemyUnit.transform.position);
                }

                break;

            default:
                throw new NotImplementedException();
                break;
        }
    }

    public bool CheckCoordinateIsInsideBoard(FieldCoordinate coordinate)
    {
        bool insideX = coordinate.X >= 0 && coordinate.X < _matchModel.Board.GetLength(0);
        bool insideY = coordinate.Y >= 0 && coordinate.Y < _matchModel.Board.GetLength(1);

        return insideX && insideY;
    }

    public FightingUnitMonoBase FindUnitOnCoordinate(MatchPlayerType matchPlayerType, FieldCoordinate coord)
    {
        foreach (var item in _matchModel.Players[matchPlayerType].FightingUnits)
        {
            bool sameCoord = _utilityMatchGeneral.Check2CoordinatesAreEqual(item.FieldCoordinate.Data, coord);
            return item;
        }

        return null;
    }

    public TowerBase FindTowerOnCoordinate(MatchPlayerType matchPlayerType, FieldCoordinate coord)
    {
        var tower = _matchModel.Players[matchPlayerType].TowerBase;
        bool sameCoord =
            _utilityMatchGeneral.Check2CoordinatesAreEqual(
                tower.FieldCoordinate, coord);
        if (sameCoord)
            return tower;
        return null;
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

    public Vector3 GetHexPanelPosition(FieldCoordinate coord)
    {
        return _matchModel.Board[coord.X, coord.Y].Position;
    }


}