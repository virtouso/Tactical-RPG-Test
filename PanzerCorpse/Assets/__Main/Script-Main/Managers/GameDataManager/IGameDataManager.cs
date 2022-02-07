using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
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