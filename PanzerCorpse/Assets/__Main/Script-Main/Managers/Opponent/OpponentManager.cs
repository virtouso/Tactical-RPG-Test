using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OpponentManager :MonoBehaviour, IOpponentManager
{
    [Inject] private BaseOpponent _opponent;
    [Inject] private IGameStateManager _gameStateManager;
    [Inject] private IUtilityMatchQueries _utilityMatchQueries;




    private void CheckMyTurn(MatchPlayerType playerTurn)
    {
        if (playerTurn != MatchPlayerType.Opponent) return;
        ActionQuery opponentSelectedAction = _opponent.ApplyAction(_utilityMatchQueries.MatchModel);
        _gameStateManager.SelectedWholeMoveByPlayers(opponentSelectedAction);
        
    }


    
    
    private void Start()
    {
        _gameStateManager.OnTurnUpdate += CheckMyTurn;
    }
    
    
}
