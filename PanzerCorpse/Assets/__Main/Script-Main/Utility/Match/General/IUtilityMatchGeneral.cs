using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUtilityMatchGeneral
{
    public Vector3 ConvertFieldCoordinateToVector3(FieldCoordinate coordinate, MatchModel matchModel);
    public int CalculateDistanceBetween2Coordinates(FieldCoordinate source, FieldCoordinate destination);

    // could use operator overriding but using such function is more understandable
    public bool Check2CoordinatesAreEqual(FieldCoordinate source, FieldCoordinate destination);

    FieldCoordinate CalculateDirection(FieldCoordinate source, FieldCoordinate destination);
    FieldCoordinate MoveToDestination(int steps, FieldCoordinate origin, FieldCoordinate destination);
    FieldCoordinate MoveByDirection(int steps, FieldCoordinate origin, FieldCoordinate direction);

    Vector2 CalculateBezier2d(Vector2 start, Vector2 control, Vector2 end, float t);

    Vector3 CalculateBezier3d(Vector3 start, Vector3 control, Vector3 end, float t);

    Vector3 CalculateLine(Vector3 start,  Vector3 end, float t);

    MatchPlayerType SwitchPlayers(MatchPlayerType currentPlayer);

}