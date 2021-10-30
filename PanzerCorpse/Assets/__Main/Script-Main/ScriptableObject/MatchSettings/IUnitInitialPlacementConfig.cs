using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitInitialPlacementConfig 
{
     Dictionary<int, FieldCoordinate> IndexCoordinates { get; }
}
