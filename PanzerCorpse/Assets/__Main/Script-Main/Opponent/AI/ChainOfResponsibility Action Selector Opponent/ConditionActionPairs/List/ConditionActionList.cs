using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ConditionActionList", menuName = "Config/Condition Action/List")]
public class ConditionActionList : ScriptableObject
{
[SerializeField]    private List<AiTypeConditionActionListPair> PlayerTypes;

public Dictionary<AiTypes, ConditionActionBase> _actionsDictionary
{
    get
    {
        
    }
    
}

public ActionQuery SelectBestAction(AiTypes aiType)
{
    
}



}
