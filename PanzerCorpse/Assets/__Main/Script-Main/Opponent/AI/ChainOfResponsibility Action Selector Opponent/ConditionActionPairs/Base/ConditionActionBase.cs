using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class ConditionActionBase : ScriptableObject
{
    [Inject] protected IUtilityMatchGeneral _generalMatchUtility;
    public abstract ActionQuery Execute(MatchModel matchState);


}
