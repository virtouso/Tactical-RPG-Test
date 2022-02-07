using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

namespace Panzers.Manager
{
  
  public interface IGameStateManager
  {
    bool PlayerCanPlay { get; }
    void PlayerCleared();
    void PlayerSelectedOrigin(FieldCoordinate coordinate);
    void SelectedWholeMoveByPlayers(ActionQuery actionQuery);
    System.Action<MatchPlayerType> OnTurnUpdate { get; set; }
    System.Action<bool, MatchPlayerType> ActionFinished { get; set; }
    System.Action<MatchPlayerType> OnGameFinished { get; set; }


  }
}