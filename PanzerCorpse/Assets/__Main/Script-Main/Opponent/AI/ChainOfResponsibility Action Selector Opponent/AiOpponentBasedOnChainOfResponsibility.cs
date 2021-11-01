using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AiOpponentBasedOnChainOfResponsibility : BaseOpponent
{
    [SerializeField] private ConditionActionList _condtionsList;
    [Inject] private AiTypes _aiType;
    public override ActionQuery ApplyAction(MatchModel matchModel)
    {
       
        
    }
}
