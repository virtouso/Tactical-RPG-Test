using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using UnityEngine;

public class HexPanel : HexPanelBase
{
    [SerializeField] private Transform _centerTransform;

    [SerializeField] private MeshRenderer _meshRenderer;
    public override FieldCoordinate FieldCoordinate { get; set; }

    public override Vector3 Position => _centerTransform.position;

    public override void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public override void UpdateMaterial(Material newMaterial)
    {
        _meshRenderer.material = newMaterial;
    }
}
