using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "ConditionActionList", menuName = "Config/Condition Action/List")]
public class ConditionActionList : ScriptableObject
{
    [FormerlySerializedAs("PlayerTypes")] [SerializeField] 
    private List<AiTypeConditionActionListPair> _playerTypes;

    private Dictionary<AiTypes,  ConditionActionBase[]> _actionsDictionary;

    public Dictionary<AiTypes, ConditionActionBase[]> ActionsDictionary
    {
        get
        {
            if (_actionsDictionary == null)
            {
                foreach (var item in  _playerTypes)
                {
                    _actionsDictionary.Add(item.AiType, item.OrderedConditionActionList);
                }
            }

            return _actionsDictionary;
        }
    }

    public ActionQuery SelectBestAction(AiTypes aiType,MatchModel matchState)
    {
        foreach (var item in ActionsDictionary[aiType])
        {
            var result = item.Execute(matchState);
            if (result != null)
                return result;
        }

        throw new NotImplementedException();
    }
}