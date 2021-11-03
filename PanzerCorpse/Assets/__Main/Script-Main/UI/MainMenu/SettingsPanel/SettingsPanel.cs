using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SettingsPanel : PanelBase
{

    [Inject] private BaseAudioManager _audioManager;


    public float MusicVolume => _audioManager.MusicVolume;

    public float AfxVolume => _audioManager.AfxVolume;


    public void UpdateMusic(float value)
    {
        _audioManager.MusicVolume = value;
    }

    public void UpdateAfx(float value)
    {
        _audioManager.AfxVolume = value;
    }




    public void BackButtonPressed()
    {
        UiManager.ShowPreviousPanel();
    }




    public override void Show(Action<object> action, object data)
    {
        
    }

    public override void Hide()
    {

    }
}
