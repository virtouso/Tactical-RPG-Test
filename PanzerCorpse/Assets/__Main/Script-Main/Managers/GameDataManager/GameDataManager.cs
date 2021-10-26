using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
                _playerData = _utilitySerializer.Deserialize<PlayerData>(_utilityFile.LoadString(PlayerPrefsGeneralKeys.PlayerDate));
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
                _gameState = _utilitySerializer.Deserialize<GameState>(_utilityFile.LoadString(PlayerPrefsGeneralKeys.GameState));
            }
            return _gameState;

        }
        set => _gameState = value;
    }










}
