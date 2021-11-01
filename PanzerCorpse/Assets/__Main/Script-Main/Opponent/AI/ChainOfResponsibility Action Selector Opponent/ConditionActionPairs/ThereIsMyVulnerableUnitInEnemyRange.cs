using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ThereIsMyVulnerableUnityInEnemyRange",
    menuName = "Config/Condition Action/ThereIsMyVulnerableUnityInEnemyRange")]
public class ThereIsMyVulnerableUnitInEnemyRange : ConditionActionBase
{
    public override ActionQuery Execute(MatchModel matchState)
    {
        var myUnits = matchState.Players[MatchPlayerType.Opponent].FightingUnits;
        var enemyUnits = matchState.Players[MatchPlayerType.Player].FightingUnits;

        FightingUnitMonoBase myVulnerableUnit = null;
        FightingUnitMonoBase selectedEnemyUnit = null;
        foreach (var myUnit in myUnits)
        {
            foreach (var enemyUnit in enemyUnits)
            {
                bool myUnitIsVulnerable =
                    myUnit.CurrentState.HealthAmount.Data <= enemyUnit.CurrentState.DamageAmount.Data;
                bool inRange = _generalMatchUtility.CalculateDistanceBetween2Coordinates(
                    myUnit.FieldCoordinate.Data, enemyUnit.FieldCoordinate.Data
                ) <= enemyUnit.CurrentState.MovingUntsInTurn.Data;

                if (inRange && myUnitIsVulnerable)
                {
                    myVulnerableUnit = myUnit;
                    selectedEnemyUnit = enemyUnit;
                    break;
                }
            }
        }

        if (myVulnerableUnit == null || selectedEnemyUnit == null)
            return null;



        for (int i = myVulnerableUnit.FieldCoordinate.Data.X-3; i < myVulnerableUnit.FieldCoordinate.Data.X+3; i++)
        {
            for (int j = myVulnerableUnit.FieldCoordinate.Data.Y-3; j < myVulnerableUnit.FieldCoordinate.Data.X+3; j++)
            {
                int newDistance = _generalMatchUtility.CalculateDistanceBetween2Coordinates(
                    selectedEnemyUnit.FieldCoordinate.Data, new FieldCoordinate(i,j));

                bool stillInDanger = newDistance <= selectedEnemyUnit.CurrentState.MovingUntsInTurn.Data;

                bool hexInBoard = _queryMatchUtility.CheckCoordinateIsInsideBoard(new FieldCoordinate(i, j));
                bool hexMasked = _queryMatchUtility.CheckHexPanelIsMasked(new FieldCoordinate(i, j));

                if (!stillInDanger && hexInBoard && !hexMasked)
                {
                    return new ActionQuery(ActionType.Move,myVulnerableUnit.FieldCoordinate.Data,new FieldCoordinate(i,j),MatchPlayerType.Opponent);
                }

            }
        }  
        
        
        return null;
    }
}