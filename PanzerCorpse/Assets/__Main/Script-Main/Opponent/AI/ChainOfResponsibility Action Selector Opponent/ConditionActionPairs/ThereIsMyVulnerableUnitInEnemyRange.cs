using System.Collections;
using System.Collections.Generic;
using Panzers.Entities;
using UnityEngine;


[CreateAssetMenu(fileName = "ThereIsMyVulnerableUnityInEnemyRange",
    menuName = "Config/Condition Action/ThereIsMyVulnerableUnityInEnemyRange")]
public class ThereIsMyVulnerableUnitInEnemyRange : ConditionActionBase
{
    public override ActionQuery Execute(MatchModel matchState,IUtilityMatchGeneral generalMatchUtility,IUtilityMatchQueries queryMatchUtility)
    {
        var myUnits = matchState.Players[MatchPlayerType.Opponent].FightingUnits;
        var enemyUnits = matchState.Players[MatchPlayerType.Player].FightingUnits;

        FightingUnitMonoBase myVulnerableUnit = null;
        FightingUnitMonoBase selectedEnemyUnit = null;
        if (FindMyVulnerableUnit(generalMatchUtility, myUnits, enemyUnits, ref myVulnerableUnit, ref selectedEnemyUnit)) 
            return null;
        
        if (FindRightPlaceToEscape(generalMatchUtility, queryMatchUtility, myVulnerableUnit, selectedEnemyUnit, out var execute)) return execute;  
        
        
        return null;
    }

    private static bool FindRightPlaceToEscape(IUtilityMatchGeneral generalMatchUtility,
        IUtilityMatchQueries queryMatchUtility, FightingUnitMonoBase myVulnerableUnit,
        FightingUnitMonoBase selectedEnemyUnit, out ActionQuery execute)
    {
        for (int i = myVulnerableUnit.FieldCoordinate.Data.X - 3; i < myVulnerableUnit.FieldCoordinate.Data.X + 3; i++)
        {
            for (int j = myVulnerableUnit.FieldCoordinate.Data.Y - 3; j < myVulnerableUnit.FieldCoordinate.Data.X + 3; j++)
            {
                int newDistance = generalMatchUtility.CalculateDistanceBetween2Coordinates(
                    selectedEnemyUnit.FieldCoordinate.Data, new FieldCoordinate(i, j));

                bool stillInDanger = newDistance <= selectedEnemyUnit.CurrentState.MovingUnitsInTurn.Data;

                bool hexInBoard = queryMatchUtility.CheckCoordinateIsInsideBoard(new FieldCoordinate(i, j));
                bool hexMasked = queryMatchUtility.CheckHexPanelIsMasked(new FieldCoordinate(i, j));

                if (!stillInDanger && hexInBoard && !hexMasked)
                {
                    {
                        execute = new ActionQuery(ActionType.Move, myVulnerableUnit.FieldCoordinate.Data,
                            new FieldCoordinate(i, j), MatchPlayerType.Opponent);
                        return true;
                    }
                }
            }
        }

        execute = null;
        return false;
    }

    private static bool FindMyVulnerableUnit(IUtilityMatchGeneral generalMatchUtility, List<FightingUnitMonoBase> myUnits, List<FightingUnitMonoBase> enemyUnits,
        ref FightingUnitMonoBase myVulnerableUnit, ref FightingUnitMonoBase selectedEnemyUnit)
    {
        foreach (var myUnit in myUnits)
        {
            foreach (var enemyUnit in enemyUnits)
            {
                bool myUnitIsVulnerable =
                    myUnit.CurrentState.HealthAmount.Data <= enemyUnit.CurrentState.DamageAmount.Data;
                bool inRange = generalMatchUtility.CalculateDistanceBetween2Coordinates(
                    myUnit.FieldCoordinate.Data, enemyUnit.FieldCoordinate.Data
                ) <= enemyUnit.CurrentState.MovingUnitsInTurn.Data;

                if (inRange && myUnitIsVulnerable)
                {
                    myVulnerableUnit = myUnit;
                    selectedEnemyUnit = enemyUnit;
                    break;
                }
            }
        }

        if (myVulnerableUnit == null || selectedEnemyUnit == null)
            return true;
        return false;
    }
}