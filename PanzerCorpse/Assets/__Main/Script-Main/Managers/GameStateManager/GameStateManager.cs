using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStateManager : MonoBehaviour, IGameStateManager
{
    public MatchModel MatchStateModel;

    [Inject] private IUtilityMatchGeneral _generalMatchUtility;
    [Inject] private IUtilityMatchQueries _matchQueryUtility;

    [Inject] private IGameDataManager _gameDataManager;
    [Inject] private IHexMapGenerator _hexMapGenerator;








    #region Main Utility

    private void StartMatch()
    {

    }
    #endregion




    #region Sub Utility

    private IEnumerator LoadThemeScene()
    {

        SceneManager.LoadScene(_gameDataManager.GetPlayerSelectedMap(), LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
    }


    private IEnumerator GenerateField()
    {
        yield return _hexMapGenerator.GenerateHexGrid();
    }


    //private IEnumerator GeneratePlayerTower()
    //{

    //}

    //private IEnumerator GenerateOpponentTower()
    //{

    //}

    //private IEnumerator GeneratePlayerUnits()
    //{

    //}

    //private IEnumerator GenerateOpponentUnits()
    //{

    //}


    #endregion






    #region Unity Callbacks

    private void Start()
    {

    }
    #endregion

}
