using System;
using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Entities;
using Panzers.Utility;
using UnityEngine;

namespace Panzers.AI
{
    [CreateAssetMenu(fileName = "NoConditionMoveNearestUnitToEnemyTowerOrShootIfAUnitHasItInItsRange",
        menuName = "Config/Condition Action/NoConditionMoveNearestUnitToEnemyTowerOrShootIfAUnitHasItInItsRange")]
    public class NoConditionMoveNearestUnitToEnemyTowerOrShootIfAUnitHasItInItsRange : ConditionActionBase
    {
        public override ActionQuery Execute(MatchModel matchState, IUtilityMatchGeneral generalMatchUtility,
            IUtilityMatchQueries queryMatchUtility)
        {
            var nearestUnit = FindMyNearestUnitToEnemyTower(matchState, generalMatchUtility, out var nearestDistance);

            if (nearestDistance <= nearestUnit.CurrentState.MovingUnitsInTurn.Data)
            {
                return new ActionQuery(ActionType.Shoot, nearestUnit.FieldCoordinate.Data,
                    matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate, MatchPlayerType.Opponent);
            }
            else
            {
                FieldCoordinate destination =
                    generalMatchUtility.MoveToDestination(nearestUnit.CurrentState.MovingUnitsInTurn.Data,
                        nearestUnit.FieldCoordinate.Data,
                        matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate);

                for (int i = destination.X - 1; i < destination.X + 1; i++)
                {
                    for (int j = destination.Y - 1; j < destination.Y + 1; j++)
                    {
                        bool inField = queryMatchUtility.CheckCoordinateIsInsideBoard(new FieldCoordinate(i, j));
                        if (!inField) continue;
                        bool isMasked = queryMatchUtility.CheckHexPanelIsMasked(new FieldCoordinate(i, j));
                        if (isMasked) continue;
                        return new ActionQuery(ActionType.Move, nearestUnit.FieldCoordinate.Data,
                            new FieldCoordinate(i, j), MatchPlayerType.Opponent);
                    }
                }

                throw new NotImplementedException();
            }
        }

        private static FightingUnitMonoBase FindMyNearestUnitToEnemyTower(MatchModel matchState,
            IUtilityMatchGeneral generalMatchUtility, out int nearestDistance)
        {
            var myUnits = matchState.Players[MatchPlayerType.Opponent].FightingUnits;
            FightingUnitMonoBase nearestUnit = myUnits[0];
            nearestDistance = generalMatchUtility.CalculateDistanceBetween2Coordinates(myUnits[0].FieldCoordinate.Data,
                matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate);
            foreach (var item in myUnits)
            {
                int distance = generalMatchUtility.CalculateDistanceBetween2Coordinates(item.FieldCoordinate.Data,
                    matchState.Players[MatchPlayerType.Player].TowerBase.FieldCoordinate);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestUnit = item;
                }
            }

            return nearestUnit;
        }
    }
}