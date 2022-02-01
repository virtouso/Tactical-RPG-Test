using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class ConditionActionBase : ScriptableObject
{
    public abstract ActionQuery Execute(MatchModel matchState,IUtilityMatchGeneral generalMatchUtility,IUtilityMatchQueries queryMatchUtility);
    
}
