using System;
using System.Collections;
using System.Collections.Generic;
using Panzers.Reference;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
public class RootPanel : PanelBase
{


    public void SinglePlayerButtonPressed()
    {
        //TODO: setup some settings
        SceneManager.LoadScene(SceneNames.SinglePlayerLogicScene);

    }




    public void SettingButtonPressed()
    {
        UiManager.ShowPanel(PanelKeys.Settings);
    }



    public void ExitButtonPressed()
    {
        //TODO: you can ask that player is sure by popup
        Application.Quit();
    }







    public override void Show(Action<object> action, object data)
    {
       
    }

    public override void Hide()
    {
       
    }
}
