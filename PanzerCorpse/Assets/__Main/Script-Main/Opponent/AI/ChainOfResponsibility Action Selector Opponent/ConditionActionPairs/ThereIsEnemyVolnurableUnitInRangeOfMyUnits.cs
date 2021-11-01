using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ThereIsEnemyVolnurableUnitInRangeOfMyUnits", menuName = "Config/Condition Action/ThereIsEnemyVolnurableUnitInRangeOfMyUnits")]
public class ThereIsEnemyVolnurableUnitInRangeOfMyUnits : ConditionActionBase
{
    public override ActionQuery Execute(MatchModel matchState)
    {
        var enemyUnits = matchState.Players[MatchPlayerType.Player].FightingUnits;
        var myUnits = matchState.Players[MatchPlayerType.Opponent].FightingUnits;

        foreach (var enemyUnit in enemyUnits)
        {
            foreach (var myUnit in myUnits)
            {
                int distance = _generalMatchUtility.CalculateDistanceBetween2Coordinates
                    (enemyUnit.FieldCoordinate.Data,myUnit.FieldCoordinate.Data);

                bool inRange = distance <= myUnit.CurrentState.MovingUntsInTurn.Data;
                bool vulnerable = enemyUnit.CurrentState.HealthAmount.Data <= myUnit.CurrentState.DamageAmount.Data;
                if (vulnerable && inRange)
                    return new ActionQuery(ActionType.Shoot,myUnit.FieldCoordinate.Data,enemyUnit.FieldCoordinate.Data,MatchPlayerType.Opponent);
            }
        }

        return null;
    }
}
