using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamePlayUiManager : MonoBehaviour, IGamePlayUiManager
{
    [Inject] private IUtilityMatchQueries _utilityMatchQueries;
    [Inject] private ITurnPopup _turnPopup;



    private void ShowTurnPopUp(MatchPlayerType matchPlayer)
    {
        _turnPopup.ShowTurnMessage(matchPlayer,_utilityMatchQueries.MatchModel.NumberOfPermittedMovesInOneTurn);
    }




    private void Start()
    {
        _utilityMatchQueries.MatchModel.Turn.Action += ShowTurnPopUp;
    }
    
}
