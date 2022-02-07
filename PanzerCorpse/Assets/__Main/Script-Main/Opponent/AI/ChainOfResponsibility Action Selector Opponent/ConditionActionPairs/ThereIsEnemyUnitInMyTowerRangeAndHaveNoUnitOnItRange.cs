using System.Collections;
using System.Collections.Generic;
using Panzers.Entities;
using UnityEngine;



[CreateAssetMenu(fileName = "ThereIsEnemyUnitInMyTowerRangeAndHaveNoUnitOnItRange", menuName = "Config/Condition Action/ThereIsEnemyUnitInMyTowerRangeAndHaveNoUnitOnItRange")]
public class ThereIsEnemyUnitInMyTowerRangeAndHaveNoUnitOnItRange : ConditionActionBase
{
    public override ActionQuery Execute(MatchModel matchState,IUtilityMatchGeneral generalMatchUtility,IUtilityMatchQueries queryMatchUtility)
    {
        FightingUnitMonoBase nearPlayerUnitToMyTower=null;
        foreach (var item in matchState.Players[MatchPlayerType.Player].FightingUnits)
        {
            if (generalMatchUtility.CalculateDistanceBetween2Coordinates(matchState.Players[MatchPlayerType.Opponent].TowerBase.FieldCoordinate, item.FieldCoordinate.Data) <= item.CurrentState.MovingUnitsInTurn.Data)
                nearPlayerUnitToMyTower = item;
        }
        if (nearPlayerUnitToMyTower == null) return null;
        FightingUnitMonoBase myNearUnitToEnemy=null;
        foreach (var item in matchState.Players[MatchPlayerType.Opponent].FightingUnits )
        {
            if (generalMatchUtility.CalculateDistanceBetween2Coordinates(item.FieldCoordinate.Data, nearPlayerUnitToMyTower.FieldCoordinate.Data) < item.CurrentState.MovingUnitsInTurn.Data)
            {
                myNearUnitToEnemy = item;
                break;
            }
        }
        if (myNearUnitToEnemy == null) return null;
        return new ActionQuery(ActionType.Shoot, myNearUnitToEnemy.FieldCoordinate.Data, nearPlayerUnitToMyTower.FieldCoordinate.Data,MatchPlayerType.Opponent);
    }
}
