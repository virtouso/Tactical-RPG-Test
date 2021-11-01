using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NoConditionMoveNearestUnitToEnemyTowerOrShootIfAUnitHasItInItsRange",
    menuName = "Config/Condition Action/NoConditionMoveNearestUnitToEnemyTowerOrShootIfAUnitHasItInItsRange")]
public class NoConditionMoveNearestUnitToEnemyTowerOrShootIfAUnitHasItInItsRange : ConditionActionBase
{
    public override ActionQuery Execute(MatchModel matchState)
    {
        var myUnits = matchState.Players[MatchPlayerType.Opponent].FightingUnits;
        FightingUnitMonoBase nearestUnit = myUnits[0];
        int nearestDistance = _generalMatchUtility.CalculateDistanceBetween2Coordinates(myUnits[0].FieldCoordinate.Data,
            matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate);
        foreach (var item in myUnits)
        {
            int distance = _generalMatchUtility.CalculateDistanceBetween2Coordinates(item.FieldCoordinate.Data,
                matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestUnit = item;
            }
        }

        if (nearestDistance <= nearestUnit.CurrentState.MovingUntsInTurn.Data)
            return new ActionQuery(ActionType.Shoot, nearestUnit.FieldCoordinate.Data,
                matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate, MatchPlayerType.Opponent);
        else
        {
            FieldCoordinate destination =
                _generalMatchUtility.MoveToDestination(nearestUnit.CurrentState.MovingUntsInTurn.Data,
                    nearestUnit.FieldCoordinate.Data,
                    matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate);
            
            for (int i = destination.X - 1; i < destination.X + 1; i++)
            {
                for (int j = destination.Y - 1; j < destination.Y + 1; j++)
                {
                    bool inField = _queryMatchUtility.CheckCoordinateIsInsideBoard(new FieldCoordinate(i, j));
                    if (!inField) continue;
                    bool isMasked = _queryMatchUtility.CheckHexPanelIsMasked(new FieldCoordinate(i, j));
                    if (isMasked) continue;
                    return new ActionQuery(ActionType.Move, nearestUnit.FieldCoordinate.Data,
                        new FieldCoordinate(i, j), MatchPlayerType.Opponent);
                }
            }
            
            throw new NotImplementedException();
        }
    }
}