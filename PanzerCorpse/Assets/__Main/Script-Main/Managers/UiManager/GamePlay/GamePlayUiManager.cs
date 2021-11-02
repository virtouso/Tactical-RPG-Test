using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamePlayUiManager : MonoBehaviour, IGamePlayUiManager
{
    [Inject] private IUtilityMatchQueries _utilityMatchQueries;
    [Inject] private ITurnPopup _turnPopup;
    [Inject] private IGameStateManager _gameStateManager;
    [Inject] private IEndPopup _endPopup;

    private void ShowTurnPopUp(MatchPlayerType matchPlayer)
    {
        _turnPopup.ShowTurnMessage(matchPlayer,_utilityMatchQueries.MatchModel.NumberOfPermittedMovesInOneTurn);
    }

    private void ShowFinalMessage(MatchPlayerType matchPlayer)
    {
        _endPopup.ShowFinalMessage(matchPlayer);
    }


    private void Start()
    {
        _utilityMatchQueries.MatchModel.Turn.Action += ShowTurnPopUp;
        _gameStateManager.OnGameFinished += ShowFinalMessage;
    }
    
}
