using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using UnityEngine;

namespace Panzers.Configurations
{
     public interface IUnitInitialPlacementConfig
     {
          Dictionary<int, FieldCoordinate> IndexCoordinates { get; }
     }
}