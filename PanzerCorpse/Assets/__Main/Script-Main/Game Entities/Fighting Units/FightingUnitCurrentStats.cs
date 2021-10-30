using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingUnitCurrentStats
{
   public Model<int> DamageAmount;
   public Model<int> HealthAmount;
   public Model<int> MovingUntsInTurn;

   public FightingUnitCurrentStats(Model<int> damageAmount, Model<int> healthAmount, Model<int> movingUntsInTurn)
   {
      DamageAmount = damageAmount;
      HealthAmount = healthAmount;
      MovingUntsInTurn = movingUntsInTurn;
   }
}
