using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMatchQueries : IUtilityMatchQueries
{
    
    public GameState GameState { get; }
    public GameState InitGameStateFields(GameState gameState)
    {
        throw new System.NotImplementedException();
    }

    public FieldCoordinate GetTowerPosition(MatchPlayerType matchPlayerType)
    {
        throw new System.NotImplementedException();
    }

    public List<FieldCoordinate> GetInitialUnitPosition(MatchPlayerType matchPlayerType)
    {
        throw new System.NotImplementedException();
    }

    public bool CheckMoveIsValid(ActionQuery actionQuery, GameState gameState)
    {
        throw new System.NotImplementedException();
    }

    public List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate, GameState gameState)
    {
        throw new System.NotImplementedException();
    }

    public MatchPlayerType CheckMatchIsFinished(GameState gameState)
    {
        throw new System.NotImplementedException();
    }

    public GameState ApplyMove(ActionQuery actionQuery, GameState gameState)
    {
        throw new System.NotImplementedException();
    }

    public GameState UpdateTurnState()
    {
        throw new System.NotImplementedException();
    }
}
