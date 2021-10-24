using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RootPanel : MonoBehaviour
{

    [Inject] private IMenuUiManager _uiManager;

    public void PlayButtonPressed()
    {

    }




    public void SettingButtonPressed()
    {

    }



    public void ExitButtonPressed()
    {
        Application.Quit();
    }


}
