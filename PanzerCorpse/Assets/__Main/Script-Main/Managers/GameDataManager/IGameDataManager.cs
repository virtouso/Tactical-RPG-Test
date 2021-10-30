using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameDataManager
{
    PlayerData PlayerData { get; set; }
    GameState GameState { get; set; }

    string GetPlayerSelectedMap();
    public Model<List<TankProgress>> GetTanksProgress();
}