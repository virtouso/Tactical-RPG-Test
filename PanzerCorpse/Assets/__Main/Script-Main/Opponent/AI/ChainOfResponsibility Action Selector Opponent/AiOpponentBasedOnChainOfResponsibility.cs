using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AiOpponentBasedOnChainOfResponsibility : BaseOpponent
{
    [SerializeField] private ConditionActionList _condtionsList;
    public override ActionQuery ApplyAction(MatchModel matchModel)
    {
        return _condtionsList.SelectBestAction(GeneralSettings.MatchSelectedAiType,matchModel,UtilityMatchGeneral,UtilityMatchQueries);
    }
}
