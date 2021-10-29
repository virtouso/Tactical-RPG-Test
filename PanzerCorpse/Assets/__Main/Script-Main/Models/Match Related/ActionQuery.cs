using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ActionQuery
{
    public ActionType ActionType;
    public FieldCoordinate Goal;
    public FieldCoordinate Current;

    public ActionQuery(ActionType actionType, FieldCoordinate current, FieldCoordinate  goal)
    {
        ActionType = actionType;
        Goal = goal;
        Current = current;
    }
}
