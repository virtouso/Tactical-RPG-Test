using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUtilityMatchQueries
{
    
    MatchModel MatchModel { get; }
    FieldCoordinate GetTowerPosition(MatchPlayerType matchPlayerType);
    bool CheckActionIsValid(ActionQuery actionQuery);
    List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate);
    MatchPlayerType CheckMatchIsFinished();
   void ApplyMove(ActionQuery actionQuery);
    void UpdateTurnState();
}