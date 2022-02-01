using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public PlayerCredentials PlayerCredentials;
    public PlayerProgress PlayerProgress;

    public PlayerData()
    {
        PlayerCredentials = new PlayerCredentials();
        PlayerProgress = new PlayerProgress();
    }
}


public class PlayerCredentials
{
    public Model<string> UniqueId;
    public Model<string> DisplayName;
    
    public PlayerCredentials()
    {
        UniqueId = new Model<string>("no_id");
        DisplayName = new Model<string>("No Name");
   
    }
    public PlayerCredentials(Model<string> uniqueId, Model<string> displayName)
    {
        UniqueId = uniqueId;
        DisplayName = displayName;
    }
}


public class PlayerProgress
{
    public Model<int> Level;
    public Model<int> Experience;
    public Model<List<TankProgress>> OwnedTanks;

    public PlayerProgress(Model<int> level, Model<int> experience, Model<List<TankProgress>> ownedTanks)
    {
        Level = level;
        Experience = experience;
        OwnedTanks = ownedTanks;
    }

    public PlayerProgress()
    {
        Level = new Model<int>(0);
        Experience = new Model<int>(0);
        InitTanksWithBaseValues();
    }

    private void InitTanksWithBaseValues()
    {
        OwnedTanks = new Model<List<TankProgress>>(new List<TankProgress>());
        OwnedTanks.Data.Add(new TankProgress(FightingUnitNamesEnum.TigerTank,0));
        OwnedTanks.Data.Add(new TankProgress(FightingUnitNamesEnum.MouseTank,0));
        OwnedTanks.Data.Add(new TankProgress(FightingUnitNamesEnum.MouseTank,0));

    }
    
    
    
}

public class TankProgress
{
    public FightingUnitNamesEnum TankId;
    public int TankLevel;

    public TankProgress(FightingUnitNamesEnum tankId, int tankLevel)
    {
        TankId = tankId;
        TankLevel = tankLevel;
    }
    public TankProgress(FightingUnitNamesEnum tankId)
    {
        TankId = tankId;
        TankLevel = 0;
    }
    
}

