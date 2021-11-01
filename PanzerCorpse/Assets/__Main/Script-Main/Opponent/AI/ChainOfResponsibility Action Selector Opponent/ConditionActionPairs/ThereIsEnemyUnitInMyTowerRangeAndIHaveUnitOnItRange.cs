using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ThereIsEnemyUnitInMyTowerRangeAndIHaveUnitOnItRange",
    menuName = "Config/Condition Action/ThereIsEnemyUnitInMyTowerRangeAndIHaveUnitOnItRange")]
public class ThereIsEnemyUnitInMyTowerRangeAndIHaveUnitOnItRange : ConditionActionBase
{
    public override ActionQuery Execute(MatchModel matchState)
    {
        FightingUnitMonoBase enemyNearMyTower = null;

        TowerBase myTower = matchState.Players[MatchPlayerType.Opponent].TowerBase;
        List<FightingUnitMonoBase> enemyUnits = matchState.Players[MatchPlayerType.Player].FightingUnits;
        foreach (var item in enemyUnits)
        {
            int distance = _generalMatchUtility.CalculateDistanceBetween2Coordinates(
                myTower.FieldCoordinate, item.FieldCoordinate.Data);
            if (distance < item.CurrentState.MovingUntsInTurn.Data)
            {
                enemyNearMyTower = item;
                break;
            }
        }

        if (enemyNearMyTower == null) return null;


        List<FightingUnitMonoBase> myUnits = matchState.Players[MatchPlayerType.Opponent].FightingUnits;

        FightingUnitMonoBase myNearestUnitToEnemy = myUnits[0];
        int minDistance = _generalMatchUtility.CalculateDistanceBetween2Coordinates(
            myNearestUnitToEnemy.FieldCoordinate.Data, enemyNearMyTower.FieldCoordinate.Data);
        foreach (var item in myUnits)
        {
            int distance = _generalMatchUtility.CalculateDistanceBetween2Coordinates(
                enemyNearMyTower.FieldCoordinate.Data,
                item.FieldCoordinate.Data);
            if (distance < minDistance)
            {
                myNearestUnitToEnemy = item;
                minDistance = distance;
            }
        }


        FieldCoordinate destination = _generalMatchUtility.MoveToDestination(
            myNearestUnitToEnemy.CurrentState.MovingUntsInTurn.Data,
            myNearestUnitToEnemy.FieldCoordinate.Data, enemyNearMyTower.FieldCoordinate.Data);

        for (int i = destination.X - 1; i < destination.X + 1; i++)
        {
            for (int j = destination.Y - 1; j < destination.Y + 1; j++)
            {
                bool inField = _queryMatchUtility.CheckCoordinateIsInsideBoard(new FieldCoordinate(i, j));
                if (!inField) continue;
                bool isMasked = _queryMatchUtility.CheckHexPanelIsMasked(new FieldCoordinate(i, j));
                if (isMasked) continue;
                return new ActionQuery(ActionType.Move, myNearestUnitToEnemy.FieldCoordinate.Data,
                    new FieldCoordinate(i, j), MatchPlayerType.Opponent);
            }
        }


        return null;
    }


 
}