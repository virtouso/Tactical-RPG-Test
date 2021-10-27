using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ActionQuery
{
    public ActionType ActionType;
    public FieldCoordinate Goal;
    public FightingUnitMonoBase FightingUnit;

    public ActionQuery(ActionType actionType, FieldCoordinate goal, FightingUnitMonoBase fightingUnit)
    {
        ActionType = actionType;
        Goal = goal;
        FightingUnit = fightingUnit;
    }
}
