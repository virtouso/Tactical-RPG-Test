using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum Channels : uint
{
    MatchState = 1 << 0,
    Ui = 1 << 1,
    MatchGraphics = 1 << 2,
    GameLogic = 1 << 3,
    Input = 1 << 4
    //.. add channels you n need
}