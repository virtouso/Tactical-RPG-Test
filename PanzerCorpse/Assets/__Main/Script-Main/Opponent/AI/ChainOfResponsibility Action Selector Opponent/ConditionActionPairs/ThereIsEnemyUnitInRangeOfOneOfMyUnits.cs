using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ThereIsEnemyUnitInRangeOfOneOfMyUnits",
    menuName = "Config/Condition Action/ThereIsEnemyUnitInRangeOfOneOfMyUnits")]
public class ThereIsEnemyUnitInRangeOfOneOfMyUnits : ConditionActionBase
{
    public override ActionQuery Execute(MatchModel matchState)
    {
        foreach (var myUnit in matchState.Players[MatchPlayerType.Opponent].FightingUnits)
        {
            foreach (var playerUnit in matchState.Players[MatchPlayerType.Player].FightingUnits)
            {
                FieldCoordinate playerUnitCoord = playerUnit.FieldCoordinate.Data;
                FieldCoordinate myUnitCoord = myUnit.FieldCoordinate.Data;

                int distance = _generalMatchUtility.CalculateDistanceBetween2Coordinates(playerUnitCoord, myUnitCoord);
                if (distance <= myUnit.CurrentState.MovingUntsInTurn.Data)
                {
                    return new ActionQuery(ActionType.Shoot,myUnitCoord,playerUnitCoord,MatchPlayerType.Opponent);
                }
            }
        }

        return null;
    }
}