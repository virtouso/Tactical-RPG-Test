using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchModel
{
    public MatchPlayerType Turn;
    public Dictionary<MatchPlayerType, MatchPlayer> Players;
    public HexPanelBase[,] Board;

    public MatchModel()
    {
        Players = new Dictionary<MatchPlayerType, MatchPlayer>(2);
        Players.Add(MatchPlayerType.Player,
            new MatchPlayer(MatchPlayerType.Player, "Not Init", 0, null, new List<FightingUnitMonoBase>()));
        Players.Add(MatchPlayerType.Opponent,
            new MatchPlayer(MatchPlayerType.Opponent, "Not Init", 0, null, new List<FightingUnitMonoBase>()));
    }
}