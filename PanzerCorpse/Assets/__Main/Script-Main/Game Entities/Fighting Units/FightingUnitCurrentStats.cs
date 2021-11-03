using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingUnitCurrentStats
{
   public Model<int> DamageAmount;
   public Model<int> HealthAmount;
   public Model<int> MovingUnitsInTurn;

   public FightingUnitCurrentStats(Model<int> damageAmount, Model<int> healthAmount, Model<int> movingUnitsInTurn)
   {
      DamageAmount = damageAmount;
      HealthAmount = healthAmount;
      MovingUnitsInTurn = movingUnitsInTurn;
   }
}
