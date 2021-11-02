using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class ConditionActionBase : ScriptableObject
{
    // [Inject] protected ;
    // [Inject] protected IUtilityMatchQueries _queryMatchUtility;
    public abstract ActionQuery Execute(MatchModel matchState,IUtilityMatchGeneral generalMatchUtility,IUtilityMatchQueries queryMatchUtility);


}
