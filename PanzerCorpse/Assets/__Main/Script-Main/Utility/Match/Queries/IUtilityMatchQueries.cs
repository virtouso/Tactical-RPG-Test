using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUtilityMatchQueries
{
    MatchModel MatchModel { get; }
    void RemoveDeadUnityFromList();
    FieldCoordinate GetTowerPosition(MatchPlayerType matchPlayerType);
    bool CheckActionIsValid(ActionQuery actionQuery);
    List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate);
    MatchPlayerType CheckMatchIsFinished();
    void ApplyMove(ActionQuery actionQuery);

    bool CheckCoordinateIsInsideBoard(FieldCoordinate coordinate);
    
    bool CheckHexPanelIsMasked(FieldCoordinate coordinate);
    void UpdateTurnState();
}