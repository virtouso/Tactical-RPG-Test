using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightingUnitsList 
{
     Dictionary<FightingUnitNamesEnum, FightingUnitConfigBase> FightingUnits { get; }
     Material OpponentMaterial { get; }
     Material PlayerMaterial { get; }
    
     
}
