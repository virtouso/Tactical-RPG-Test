using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    public abstract void Init(Vector3 position, FieldCoordinate coordinate, TowerCurrentStats initStats);

    public Vector3 Position { get; }
    public FieldCoordinate FieldCoordinate { get; }
    public TowerCurrentStats TowerCurrentStats { get; set; }
}