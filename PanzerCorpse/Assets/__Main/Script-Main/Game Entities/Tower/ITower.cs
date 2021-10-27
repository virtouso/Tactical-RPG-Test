using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    void Init(Vector3 position, FieldCoordinate coordinate);

    Vector3 Position { get; }
    FieldCoordinate FieldCoordinate { get; }

}
