using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Utility;
using UnityEngine;
using Zenject;

namespace Panzers.AI
{
    public abstract class ConditionActionBase : ScriptableObject
    {
        public abstract ActionQuery Execute(MatchModel matchState, IUtilityMatchGeneral generalMatchUtility,
            IUtilityMatchQueries queryMatchUtility);

    }
}