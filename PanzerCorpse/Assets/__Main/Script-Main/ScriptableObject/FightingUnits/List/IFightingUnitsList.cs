using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Configurations
{
     public interface IFightingUnitsList
     {
          Dictionary<FightingUnitNamesEnum, FightingUnitConfigBase> FightingUnits { get; }
          Material OpponentMaterial { get; }
          Material PlayerMaterial { get; }

     }
}
