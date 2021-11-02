using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum MatchPlayerType : byte
{
    Player = 0,
    Opponent = 1,
    None = 2,
    Both = 254
}
