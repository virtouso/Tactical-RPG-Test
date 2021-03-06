using System.Collections;
using System.Collections.Generic;
using Mvvm;
using UnityEngine;

namespace Panzers.Entities
{
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
}