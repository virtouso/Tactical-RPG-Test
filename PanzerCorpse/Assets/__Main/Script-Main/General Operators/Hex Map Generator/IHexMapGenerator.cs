using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Operators
{
    
    public interface IHexMapGenerator
    {
        IEnumerator GenerateHexGrid();
        HexPanelBase[,] HexGrid { get; }

    }
}