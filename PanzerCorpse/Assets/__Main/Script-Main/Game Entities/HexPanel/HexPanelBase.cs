using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using UnityEngine;

public abstract class HexPanelBase : MonoBehaviour
{

    public abstract FieldCoordinate FieldCoordinate { get; set; }
    public abstract Vector3 Position { get; }

    public abstract void SetPosition(Vector3 position);

    public abstract void UpdateMaterial(Material newMaterial);

}


