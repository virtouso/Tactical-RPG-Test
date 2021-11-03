using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatchGeneralSettings
{
    AiTypes MatchSelectedAiType { get; }

    float UnitsMoveSpeed { get; }
    public Material NormalMaterial { get; }
    public Material AttackMaterial { get; }
    public Material MoveMaterial { get; }

    float LongWaitTime { get; }
    float AverageWaitTime { get; }
    float ShortWaitTime { get; }
    int TowersInitialHealth { get; }
}