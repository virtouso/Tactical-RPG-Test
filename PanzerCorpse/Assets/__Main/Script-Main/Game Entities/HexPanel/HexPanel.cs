using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPanel : HexPanelBase
{
    [SerializeField] private Transform _centerTransform;


    public override FieldCoordinate FieldCoordinate { get; set; }

    public override Vector3 Position
    {
        get => _centerTransform.position;
    }

    public override void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
