using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]
public class FightingUnitPerLevelStats
{
    public int DamageAmount;
    public int HealthAmount;
    [FormerlySerializedAs("MovingUnitsInATurn")] public int MovingUnitsInTurn;
}
