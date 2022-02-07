using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Panzers.Manager
{

    public class OpponentManager : MonoBehaviour, IOpponentManager
    {
        [Inject] private BaseOpponent _opponent;
        [Inject] private IGameStateManager _gameStateManager;
        [Inject] private IUtilityMatchQueries _utilityMatchQueries;
        [Inject] private ILogger _logger;



        private void CheckMyTurn(MatchPlayerType playerTurn)
        {
            if (playerTurn != MatchPlayerType.Opponent) return;
            _logger.ShowNormalLog("its Opponent turn", Color.blue, Channels.MatchState);
            StartCoroutine(ApplyActionWaited());

        }

        private IEnumerator ApplyActionWaited()
        {
            yield return new WaitForSeconds(5f);
            ActionQuery opponentSelectedAction = _opponent.ApplyAction(_utilityMatchQueries.MatchModel);
            _gameStateManager.SelectedWholeMoveByPlayers(opponentSelectedAction);
        }

        private void OnMoveFinished(bool valid, MatchPlayerType playerType)
        {
            if (valid) return;
            if (playerType != MatchPlayerType.Opponent) return;
            Debug.Log(" Opponent action was no valid, do it again");
            CheckMyTurn(playerType);
        }


        private void Start()
        {
            _gameStateManager.OnTurnUpdate += CheckMyTurn;
        }


    }
}
