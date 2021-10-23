using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public PlayerCredentials PlayerCredentials;
    public PlayerProgress PlayerProgress;
}




public class PlayerCredentials
{
    public Model<string> UniqueId;
    public Model<string> DisplayName;
}


public class PlayerProgress
{
    public Model<int> Level;
    public Model<int> Experience;
    public Model<List<TankProgress>> OwnedTanks;
}

public class TankProgress
{
    public string TankId;
    public int TankLevel;
}

