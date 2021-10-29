using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUtilityMatchQueries
{

    GameState GameState { get; }
    GameState InitGameStateFields(GameState gameState);
    FieldCoordinate GetTowerPosition(MatchPlayerType matchPlayerType);
    List<FieldCoordinate> GetInitialUnitPosition(MatchPlayerType matchPlayerType);
    bool CheckMoveIsValid(ActionQuery actionQuery, GameState gameState);
    List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate, GameState gameState);
    MatchPlayerType CheckMatchIsFinished(GameState gameState);
    GameState ApplyMove(ActionQuery actionQuery, GameState gameState);
    GameState UpdateTurnState();
}