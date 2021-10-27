using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ConditionActionList", menuName = "Config/Condition Action/List")]
public class ConditionActionList : ScriptableObject
{
    public List<AiTypeConditionActionListPair> PlayerTypes;
}
