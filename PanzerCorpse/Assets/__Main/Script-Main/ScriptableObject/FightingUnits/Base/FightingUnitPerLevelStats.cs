using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Panzers.Configurations
{
    [System.Serializable]
    public class FightingUnitPerLevelStats
    {
        public int DamageAmount;
        public int HealthAmount;
        public int MovingUnitsInTurn;
    }
}