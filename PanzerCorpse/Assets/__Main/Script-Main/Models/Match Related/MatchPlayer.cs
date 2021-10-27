using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayer
{
    public MatchPlayerType PlayerType;
    public string Name;
    public int Score;


    public MatchPlayer(MatchPlayerType playerType, string name, int score)
    {
        PlayerType = playerType;
        Name = name;
        Score = score;
    }
}
