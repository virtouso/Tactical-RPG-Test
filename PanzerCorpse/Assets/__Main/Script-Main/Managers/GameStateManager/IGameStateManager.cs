using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public interface IGameStateManager 
{
  bool PlayerCanPlay { get; }


  void PlayerCleared();

  void PlayerSelectedOrigin(FieldCoordinate coordinate);


  void SelectedWholeMoveByPlayers(ActionQuery actionQuery);
  System.Action<MatchPlayerType> OnTurnUpdate { get; set; }
  System.Action<bool> ActionFinished { get; }
  System.Action<MatchPlayerType> OnGameFinished { get; }


}
