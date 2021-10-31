using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public interface IGameStateManager 
{
  bool PlayerCanPlay { get; }


  void PlayerCleared();

  void PlayerSelectedOrigin(FieldCoordinate coordinate);


  void PlayerSelectedWholeMove(ActionQuery actionQuery);





}
