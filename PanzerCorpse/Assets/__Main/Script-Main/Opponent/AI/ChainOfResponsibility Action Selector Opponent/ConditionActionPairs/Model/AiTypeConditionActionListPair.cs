using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.AI
{

    [System.Serializable]
    public class AiTypeConditionActionListPair
    {
        public AiTypes AiType;
        public ConditionActionBase[] OrderedConditionActionList;
    }
}