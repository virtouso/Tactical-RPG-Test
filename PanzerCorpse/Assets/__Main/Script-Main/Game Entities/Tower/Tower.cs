using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour,ITower
{
    public void Init(Vector3 position, FieldCoordinate coordinate)
    {
        transform.position = position;
        _fieldCoordinate = coordinate;
    }

    public Vector3 Position => transform.position;

    private FieldCoordinate _fieldCoordinate;
    public FieldCoordinate FieldCoordinate => FieldCoordinate;
}
