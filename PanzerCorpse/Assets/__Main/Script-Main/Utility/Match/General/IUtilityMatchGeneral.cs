using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUtilityMatchGeneral
{
    public Vector3 ConvertFieldCoordinateToVector3(FieldCoordinate coordinate, MatchModel matchModel);
    public int CalculateDistanceBetween2Coordinates(FieldCoordinate source, FieldCoordinate destination);

    // could use operator overriding but using such function is more understandable
    public bool Check2CoordinatesAreEqual(FieldCoordinate source, FieldCoordinate destination);

    
    
}