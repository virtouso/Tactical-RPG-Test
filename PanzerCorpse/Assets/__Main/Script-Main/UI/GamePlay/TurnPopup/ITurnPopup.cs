using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnPopup
{
  IEnumerator ShowTurnMessage(MatchPlayerType playerTurn,int possibleMoves );
}
