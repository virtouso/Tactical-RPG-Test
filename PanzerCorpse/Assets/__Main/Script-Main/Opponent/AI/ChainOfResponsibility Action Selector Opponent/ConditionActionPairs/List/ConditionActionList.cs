using System;
using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Panzers.AI
{
    [CreateAssetMenu(fileName = "ConditionActionList", menuName = "Config/Condition Action/List")]
    public class ConditionActionList : ScriptableObject
    {
        [FormerlySerializedAs("PlayerTypes")] [SerializeField]
        private List<AiTypeConditionActionListPair> _playerTypes;

        private Dictionary<AiTypes, ConditionActionBase[]> _actionsDictionary;

        public Dictionary<AiTypes, ConditionActionBase[]> ActionsDictionary
        {
            get
            {
                if (_actionsDictionary == null)
                {
                    _actionsDictionary = new Dictionary<AiTypes, ConditionActionBase[]>(_playerTypes.Count);
                    foreach (var item in _playerTypes)
                    {
                        _actionsDictionary.Add(item.AiType, item.OrderedConditionActionList);
                    }
                }

                return _actionsDictionary;
            }
        }

        public ActionQuery SelectBestAction(AiTypes aiType, MatchModel matchState,
            IUtilityMatchGeneral generalMatchUtility, IUtilityMatchQueries queryMatchUtility)
        {
            foreach (var item in ActionsDictionary[aiType])
            {
                var result = item.Execute(matchState, generalMatchUtility, queryMatchUtility);
                if (result != null)
                    return result;
            }

            throw new NotImplementedException();
        }
    }
}