using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionActionBase : ScriptableObject
{

    public abstract ActionQuery Execute(MatchModel matchState);


}
