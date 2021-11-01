using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AiOpponentBasedOnChainOfResponsibility : BaseOpponent
{
    [SerializeField] private ConditionActionList _condtionsList;
    [Inject] private IMatchGeneralSettings   _generalSettings;
    public override ActionQuery ApplyAction(MatchModel matchModel)
    {
        return _condtionsList.SelectBestAction(_generalSettings.MatchSelectedAiType,matchModel);
    }
}
