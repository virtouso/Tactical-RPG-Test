using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "Fighting Units List", menuName = "Config/Fighting Units/Tanks/Fighting Units List")]
public class FightingUnitsList : ScriptableObject
{
    [SerializeField] private List<FightingUnitConfigBase> _fightingUnitsList;


    private Dictionary<FightingUnitNamesEnum, FightingUnitConfigBase> _fightingUnits;

    public Dictionary<FightingUnitNamesEnum, FightingUnitConfigBase> FightingUnits
    {
        set => _fightingUnits = value;
        get
        {
            if (_fightingUnits == null)
            {
                _fightingUnits =
                    new Dictionary<FightingUnitNamesEnum, FightingUnitConfigBase>(_fightingUnitsList.Count);
                //as list is short and it happens once for readablity foreach is better than for
                foreach (var item in _fightingUnitsList)
                {
                    _fightingUnits.Add(item.Name, item);
                }
            }

            return _fightingUnits;
        }
    }

     public Tower playerTowerBase;
    public Tower enemyTowerBase;
}