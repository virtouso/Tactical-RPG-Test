using System.Collections;
using System.Collections.Generic;
using Panzers.Configurations;
using Panzers.DataModel;
using Panzers.Utility;
using UnityEngine;
using Zenject;

namespace Panzers.Opponent
{
    public abstract class BaseOpponent : MonoBehaviour
    {
        [Inject] protected IMatchGeneralSettings GeneralSettings;
        [Inject] protected IUtilityMatchGeneral UtilityMatchGeneral;
        [Inject] protected IUtilityMatchQueries UtilityMatchQueries;

        public abstract ActionQuery ApplyAction(MatchModel matchModel);
    }
}