using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightingUnitConfigBase : ScriptableObject
{
    public FightingUnitNamesEnum Name;
    public Sprite DisplaySprite;
    public FightingUnitMonoBase GameObject;
    public FightingUnitPerLevelStats[] Stats;


}
