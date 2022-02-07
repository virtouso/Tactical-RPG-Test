using System;
using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Manager;
using UnityEngine;
using Zenject;

namespace Panzers.Input
{
    public class PlayerInputMediator : MonoBehaviour
    {
        [Inject] private InputHandlerBase _inputHandler;
        [Inject] private IGameStateManager _gameStateManager;
        [Inject] private ILogger _logger;
        [SerializeField] private LayerMask _panelLayerMask;
        private Camera _camera;


        private ActionQuery _cachedQuery;


        private Action _playerCleared;
        private Action<FieldCoordinate> _playerSelectedOrigin;
        private Action<ActionQuery> _playerSelectedWholeAction;

        private void OnPointerClicked()
        {
            if (!_gameStateManager.PlayerCanPlay) return;

            _logger.ShowNormalLog("pointer clicked called", Color.magenta, Channels.Input);
            HexPanelBase selectedPanel = SelectHexPanelUnderMouse();
            if (selectedPanel == null)
            {
                _cachedQuery.Current = null;
                _cachedQuery.Goal = null;
                _playerCleared.Invoke();
            }
            else
            {
                if (_cachedQuery.Current == null && _cachedQuery.Goal == null)
                {
                    _logger.ShowNormalLog("player first action", Color.yellow, Channels.Input);
                    _cachedQuery.Current = selectedPanel.FieldCoordinate;
                    _playerSelectedOrigin.Invoke(selectedPanel.FieldCoordinate);
                }
                else if (_cachedQuery.Current != null && _cachedQuery.Goal == null)
                {
                    _logger.ShowNormalLog("player complete action", Color.yellow, Channels.Input);

                    _cachedQuery.Goal = selectedPanel.FieldCoordinate;
                    _playerSelectedWholeAction.Invoke(_cachedQuery);

                    _cachedQuery.Current = null;
                    _cachedQuery.Goal = null;
                }
            }
        }


        private void OnMoveFinished(bool valid, MatchPlayerType playerType)
        {
        }


        #region Utility

        private HexPanelBase SelectHexPanelUnderMouse()
        {
            Ray ray = _camera.ScreenPointToRay(_inputHandler.PointerPosition);
            bool hitPanel = Physics.Raycast(ray, out RaycastHit hit, _panelLayerMask);
            if (!hitPanel) return null;
            var result = hit.collider.transform.parent.GetComponent<HexPanel>();
            return result;
        }

        #endregion


        #region Unity Callbacks

        private void Start()
        {
            _cachedQuery = new ActionQuery(ActionType.None, null, null, MatchPlayerType.Player);
            _inputHandler.PointerClicked += OnPointerClicked;
            _camera = Camera.main;
            _playerCleared += _gameStateManager.PlayerCleared;
            _playerSelectedOrigin += _gameStateManager.PlayerSelectedOrigin;
            _playerSelectedWholeAction += _gameStateManager.SelectedWholeMoveByPlayers;
            _gameStateManager.ActionFinished += OnMoveFinished;
        }

        #endregion
    }
}