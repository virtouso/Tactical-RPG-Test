using System.Collections;
using System.Collections.Generic;
using Mvvm;
using UnityEngine;

public class TowerCurrentStats
{
    public Model<int> Health;

    public TowerCurrentStats(Model<int> health)
    {
        Health = health;
    }
}
