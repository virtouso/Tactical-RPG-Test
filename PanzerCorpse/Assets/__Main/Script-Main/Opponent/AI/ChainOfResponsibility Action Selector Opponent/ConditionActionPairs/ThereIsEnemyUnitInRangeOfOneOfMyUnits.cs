using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Utility;
using UnityEngine;

namespace Panzers.AI
{
    [CreateAssetMenu(fileName = "ThereIsEnemyUnitInRangeOfOneOfMyUnits",
        menuName = "Config/Condition Action/ThereIsEnemyUnitInRangeOfOneOfMyUnits")]
    public class ThereIsEnemyUnitInRangeOfOneOfMyUnits : ConditionActionBase
    {
        public override ActionQuery Execute(MatchModel matchState, IUtilityMatchGeneral generalMatchUtility,
            IUtilityMatchQueries queryMatchUtility)
        {
            foreach (var myUnit in matchState.Players[MatchPlayerType.Opponent].FightingUnits)
            {
                foreach (var playerUnit in matchState.Players[MatchPlayerType.Player].FightingUnits)
                {
                    FieldCoordinate playerUnitCoord = playerUnit.FieldCoordinate.Data;
                    FieldCoordinate myUnitCoord = myUnit.FieldCoordinate.Data;

                    int distance =
                        generalMatchUtility.CalculateDistanceBetween2Coordinates(playerUnitCoord, myUnitCoord);
                    if (distance <= myUnit.CurrentState.MovingUnitsInTurn.Data)
                    {
                        return new ActionQuery(ActionType.Shoot, myUnitCoord, playerUnitCoord,
                            MatchPlayerType.Opponent);
                    }
                }
            }

            return null;
        }
    }
}