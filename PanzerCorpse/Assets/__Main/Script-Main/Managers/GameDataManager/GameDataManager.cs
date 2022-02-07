using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using Panzers.Reference;
using Panzers.Utility;
using UnityEngine;
using Zenject;

namespace Panzers.Manager
{
    public class GameDataManager : MonoBehaviour, IGameDataManager
    {
        [Inject] private IUtilitySerializer _utilitySerializer;
        [Inject] private IUtilityFile _utilityFile;

        private PlayerData _playerData;

        public PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    if (_utilityFile.KeyExist(PlayerPrefsGeneralKeys.PlayerDate))
                    {
                        _playerData =
                            _utilitySerializer.Deserialize<PlayerData>(
                                _utilityFile.LoadString(PlayerPrefsGeneralKeys.PlayerDate));
                    }
                    else
                    {
                        _playerData = new PlayerData();
                    }
                }

                return _playerData;
            }
            set => _playerData = value;
        }



        private GameState _gameState;

        public GameState GameState
        {
            get
            {
                if (_gameState == null)
                {

                    if (!_utilityFile.KeyExist(PlayerPrefsGeneralKeys.GameState))
                    {
                        _gameState = new GameState(SceneNames.DesertThemeScene);
                    }
                    else
                    {
                        string serializedGameState = _utilityFile.LoadString(PlayerPrefsGeneralKeys.GameState);
                        _gameState = _utilitySerializer.Deserialize<GameState>(serializedGameState);
                    }
                }

                return _gameState;
            }
            set => _gameState = value;
        }



        public string GetPlayerSelectedMap()
        {
            string selectedMap = GameState.SelectedMap;
            return selectedMap;
        }
    }
}