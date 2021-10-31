using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiOpponentBasedOnChainOfResponsibility : BaseOpponent
{
    [SerializeField] private ConditionActionList _condtionsList;
    public override ActionQuery ApplyAction(GameState gameState)
    {
        throw new System.NotImplementedException();
    }
}
