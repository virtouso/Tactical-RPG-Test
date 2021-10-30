using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMatchGeneral : IUtilityMatchGeneral
{
    public Vector3 ConvertFieldCoordinateToVector3(FieldCoordinate coordinate ,MatchModel matchModel)
    {
        return matchModel.Board[coordinate.X, coordinate.Y].Position;
    }

    
    //this calculates manhattan distance
    public int CalculateDistanceBetween2Coordinates(FieldCoordinate source, FieldCoordinate destination)
    {
        return Mathf.Abs(source.X - destination.X) + Mathf.Abs(source.Y- destination.Y);
    }
}
