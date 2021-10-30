using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMatchQueries : IUtilityMatchQueries
{
    private MatchModel _matchModel;

    public MatchModel MatchModel
    {
        get
        {
            if (_matchModel == null)
                _matchModel = new MatchModel();
            return _matchModel;
        }
        
        
    }

    public MatchModel InitGameStateFields(MatchModel matchModel)
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

    public bool CheckMoveIsValid(ActionQuery actionQuery)
    {
        throw new System.NotImplementedException();
    }

    public List<ActionQuery> ListOfLegitMovesForCoordinate(FieldCoordinate coordinate)
    {
        throw new System.NotImplementedException();
    }

    public MatchPlayerType CheckMatchIsFinished()
    {
        throw new System.NotImplementedException();
    }

    public MatchModel ApplyMove(ActionQuery actionQuery)
    {
        throw new System.NotImplementedException();
    }

    public MatchModel UpdateTurnState()
    {
        throw new System.NotImplementedException();
    }




    #region Utility

    private void InitMatchModel()
    {
        
    }

    #endregion
}