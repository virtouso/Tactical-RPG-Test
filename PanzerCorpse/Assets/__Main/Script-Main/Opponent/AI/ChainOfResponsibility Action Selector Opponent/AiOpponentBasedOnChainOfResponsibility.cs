using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Opponent;
using UnityEngine;
using Zenject;

namespace Panzers.AI
{
    
    public class AiOpponentBasedOnChainOfResponsibility : BaseOpponent
    {
        [SerializeField] private ConditionActionList _condtionsList;

        public override ActionQuery ApplyAction(MatchModel matchModel)
        {
            return _condtionsList.SelectBestAction(GeneralSettings.MatchSelectedAiType, matchModel, UtilityMatchGeneral,
                UtilityMatchQueries);
        }
    }
}