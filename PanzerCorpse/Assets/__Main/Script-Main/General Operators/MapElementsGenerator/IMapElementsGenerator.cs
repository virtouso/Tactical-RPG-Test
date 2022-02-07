using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Operators
{
    public interface IMapElementsGenerator
    {

        IEnumerator GeneratePlayerTower();
        IEnumerator GeneratePlayerUnits();
        IEnumerator GenerateOpponentUnits();
        IEnumerator GenerateOpponentTower();
    }
}