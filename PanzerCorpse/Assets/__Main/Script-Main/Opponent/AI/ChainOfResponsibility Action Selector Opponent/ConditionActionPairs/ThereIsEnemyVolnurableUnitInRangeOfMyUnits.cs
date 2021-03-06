using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Utility;
using UnityEngine;


namespace Panzers.AI
{
    [CreateAssetMenu(fileName = "ThereIsEnemyVolnurableUnitInRangeOfMyUnits",
        menuName = "Config/Condition Action/ThereIsEnemyVolnurableUnitInRangeOfMyUnits")]
    public class ThereIsEnemyVolnurableUnitInRangeOfMyUnits : ConditionActionBase
    {
        public override ActionQuery Execute(MatchModel matchState, IUtilityMatchGeneral generalMatchUtility,
            IUtilityMatchQueries queryMatchUtility)
        {
            var enemyUnits = matchState.Players[MatchPlayerType.Player].FightingUnits;
            var myUnits = matchState.Players[MatchPlayerType.Opponent].FightingUnits;

            foreach (var enemyUnit in enemyUnits)
            {
                foreach (var myUnit in myUnits)
                {
                    int distance = generalMatchUtility.CalculateDistanceBetween2Coordinates
                        (enemyUnit.FieldCoordinate.Data, myUnit.FieldCoordinate.Data);

                    bool inRange = distance <= myUnit.CurrentState.MovingUnitsInTurn.Data;
                    bool vulnerable = enemyUnit.CurrentState.HealthAmount.Data <= myUnit.CurrentState.DamageAmount.Data;
                    if (vulnerable && inRange)
                        return new ActionQuery(ActionType.Shoot, myUnit.FieldCoordinate.Data,
                            enemyUnit.FieldCoordinate.Data, MatchPlayerType.Opponent);
                }
            }

            return null;
        }
    }
}