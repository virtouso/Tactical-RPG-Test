using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Entities;
using UnityEngine;

namespace Panzers.Utility
{
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
        FightingUnitMonoBase FindUnitOnCoordinate(MatchPlayerType matchPlayerType, FieldCoordinate coord);
        TowerBase FindTowerOnCoordinate(MatchPlayerType matchPlayerType, FieldCoordinate coord);
        bool CheckHexPanelIsMasked(FieldCoordinate coordinate);
        Vector3 GetHexPanelPosition(FieldCoordinate coord);

    }
}