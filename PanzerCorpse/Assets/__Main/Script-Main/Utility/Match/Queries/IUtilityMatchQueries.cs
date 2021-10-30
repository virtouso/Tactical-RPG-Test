using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUtilityMatchQueries
{
    
    MatchModel MatchModel { get; }
    MatchModel InitGameStateFields(MatchModel matchModel);
    FieldCoordinate GetTowerPosition(MatchPlayerType matchPlayerType);
    List<FieldCoordinate> GetInitialUnitPosition(MatchPlayerType matchPlayerType);
    bool CheckMoveIsValid(ActionQuery actionQuery);
    List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate);
    MatchPlayerType CheckMatchIsFinished();
    MatchModel ApplyMove(ActionQuery actionQuery);
    MatchModel UpdateTurnState();
}