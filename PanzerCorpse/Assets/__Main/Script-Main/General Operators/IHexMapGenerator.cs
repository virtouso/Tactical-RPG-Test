using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHexMapGenerator
{
    IEnumerator GenerateHexGrid();
     HexPanelBase[,] HexGrid { get; } 
    
}
