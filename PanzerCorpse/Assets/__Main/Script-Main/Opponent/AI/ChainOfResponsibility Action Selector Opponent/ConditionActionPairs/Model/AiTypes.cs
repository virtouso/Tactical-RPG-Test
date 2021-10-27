using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiTypes 
{
    Defensive,//defending the the tower is priority
    Rational,// simply tries to do good actions
    UnitDestroyer, // first tries to destroy enemy units
    Finisher// just shooting the enemy tower and finish the match


}
