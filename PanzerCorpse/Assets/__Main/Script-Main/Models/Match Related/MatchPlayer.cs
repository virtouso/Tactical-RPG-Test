using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayer
{
    public MatchPlayerType PlayerType;
    public string Name;
    public int Score;
    public ITower Tower;
    public List<FightingUnitMonoBase> FightingUnits;

    public MatchPlayer(MatchPlayerType playerType, string name, int score, List<FightingUnitMonoBase> fightingUnits)
    {
        PlayerType = playerType;
        Name = name;
        Score = score;
        FightingUnits = fightingUnits;
    }
}
