using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Fighting Units List", menuName = "Config/Fighting Units/Tanks/Fighting Units List")]
public class FightingUnitsList : ScriptableObject
{
    public List<FightingUnitConfigBase> FightingUnits;
}
