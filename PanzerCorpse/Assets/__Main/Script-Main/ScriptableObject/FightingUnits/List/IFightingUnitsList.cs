using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightingUnitsList 
{
     Dictionary<FightingUnitNamesEnum, FightingUnitConfigBase> FightingUnits { get; }
     public Material OpponentMaterial { get; }
     public Material PlayerMaterial { get; }
     
}
