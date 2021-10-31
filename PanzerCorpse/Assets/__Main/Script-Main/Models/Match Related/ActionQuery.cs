using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ActionQuery
{
    public ActionType ActionType;
    public FieldCoordinate Goal;
    public FieldCoordinate Current;
    public MatchPlayerType From;
    public ActionQuery(  ActionType actionType, FieldCoordinate current, FieldCoordinate  goal,  MatchPlayerType from)
    {
        ActionType = actionType;
        Goal = goal;
        Current = current;
        From = from;
    }
}
