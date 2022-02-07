using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Manager
{
    public interface IGameDataManager
    {
        PlayerData PlayerData { get; set; }
        GameState GameState { get; set; }
        string GetPlayerSelectedMap();

    }
}