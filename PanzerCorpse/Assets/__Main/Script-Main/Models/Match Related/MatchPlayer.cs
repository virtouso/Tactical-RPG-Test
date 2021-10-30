using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayer
{
    public MatchPlayerType PlayerType;
    public string Name;
    public int Score;
    public TowerBase TowerBase;
    public List<FightingUnitMonoBase> FightingUnits;


 
    public MatchPlayer(MatchPlayerType playerType, string name, int score, TowerBase towerBase, List<FightingUnitMonoBase> fightingUnits)
    {
        PlayerType = playerType;
        Name = name;
        Score = score;
        TowerBase = towerBase;
        FightingUnits = fightingUnits;
    }
}
