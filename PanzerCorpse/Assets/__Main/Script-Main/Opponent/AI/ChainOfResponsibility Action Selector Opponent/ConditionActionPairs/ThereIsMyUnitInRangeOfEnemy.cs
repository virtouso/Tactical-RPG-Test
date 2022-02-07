using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Utility;
using UnityEngine;


namespace Panzers.AI
{
    [CreateAssetMenu(fileName = "ThereIsMyUnitInRangeOfEnemy",
        menuName = "Config/Condition Action/ThereIsMyUnitInRangeOfEnemy")]
    public class ThereIsMyUnitInRangeOfEnemy : ConditionActionBase
    {
        public override ActionQuery Execute(MatchModel matchState, IUtilityMatchGeneral generalMatchUtility,
            IUtilityMatchQueries queryMatchUtility)
        {
            throw new System.NotImplementedException();
        }
    }
}