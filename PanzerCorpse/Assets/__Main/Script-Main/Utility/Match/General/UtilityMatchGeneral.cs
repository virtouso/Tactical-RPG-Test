using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMatchGeneral : IUtilityMatchGeneral
{
    public Vector3 ConvertFieldCoordinateToVector3(FieldCoordinate coordinate, MatchModel matchModel)
    {
        return matchModel.Board[coordinate.X, coordinate.Y].Position;
    }


    //this calculates manhattan distance
    public int CalculateDistanceBetween2Coordinates(FieldCoordinate source, FieldCoordinate destination)
    {
        return Mathf.Abs(source.X - destination.X) + Mathf.Abs(source.Y - destination.Y);
    }

    public bool Check2CoordinatesAreEqual(FieldCoordinate source, FieldCoordinate destination)
    {
        return source.X == destination.X && source.Y == destination.Y;
    }

    public FieldCoordinate CalculateDirection(FieldCoordinate source, FieldCoordinate destination)
    {
        int xDirection = CalculateStepValue(destination.X - source.X);
        int yDirection = CalculateStepValue(destination.Y - source.Y);
        return new FieldCoordinate(xDirection, yDirection);
    }


    public FieldCoordinate MoveToDestination(int steps, FieldCoordinate origin, FieldCoordinate destination)
    {
        FieldCoordinate direction =
            CalculateDirection(origin, destination);

        FieldCoordinate path = origin;

        for (int i = 0; i < steps; i++)
        {
            if (path.X != destination.X)
            {
                path.X += direction.X;
            }
            else
            {
                path.Y += direction.Y;
            }
        }
        
        return path;
    }

    public FieldCoordinate MoveByDirection(int steps, FieldCoordinate origin, FieldCoordinate direction)
    {
        FieldCoordinate path = origin;

        for (int i = 0; i < steps; i++)
        {
            path.X += direction.X;
            path.Y += direction.Y;
        }


        return path;
    }

    Vector2 IUtilityMatchGeneral.CalculateBezier2d(Vector2 start, Vector2 control, Vector2 end, float t)
    {
        return CalculateBezier2d(start, control, end, t);
    }

    Vector3 IUtilityMatchGeneral.CalculateBezier3d(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return CalculateBezier3d(start, control, end, t);
    }

    public Vector3 CalculateLine(Vector3 start, Vector3 end, float t)
    {
        return Vector3.Lerp(start, end, t);
    }

    public MatchPlayerType SwitchPlayers(MatchPlayerType currentPlayer)
    {
        return ~(currentPlayer ^ MatchPlayerType.Both);
    }

    public static Vector2 CalculateBezier2d(Vector2 start, Vector2 control, Vector2 end, float t)
    {
        return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
    }

    public static Vector3 CalculateBezier3d(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
    }


    private int CalculateStepValue(int input)
    {
        if (input > 0) return 1;
        if (input < 0) return -1;
        return 0;
    }
}