using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ThereIsEnemyTowerInRangeOfOneOfMyUnits", menuName = "Config/Condition Action/ThereIsEnemyTowerInRangeOfOneOfMyUnits")]
public class ThereIsEnemyTowerInRangeOfOneOfMyUnits : ConditionActionBase
{
    // could cache to variables to make lines shorter.
    public override ActionQuery Execute(MatchModel matchState)
    {
        foreach (var item in matchState.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            int distanceToTower = _generalMatchUtility.
                CalculateDistanceBetween2Coordinates(item.FieldCoordinate.Data
                , matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate);

            if (distanceToTower <= item.CurrentState.MovingUntsInTurn.Data)
                return new ActionQuery(ActionType.Shoot, item.FieldCoordinate.Data,
                    matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate,MatchPlayerType.Opponent);
        }

        return null;
    }
}
