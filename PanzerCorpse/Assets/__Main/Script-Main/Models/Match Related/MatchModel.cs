using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchModel
{
    public MatchPlayerType Turn;
    public Dictionary<MatchPlayerType, MatchPlayer> Players;
    public HexPanelBase[,] Board;

}
