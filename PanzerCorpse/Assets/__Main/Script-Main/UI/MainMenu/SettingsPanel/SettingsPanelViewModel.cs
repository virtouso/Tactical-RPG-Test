using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelViewModel : ViewModelBase
{
    [SerializeField] private Slider _effectSlider;
    [SerializeField] private Slider _musicSlider;


    protected override void BindDependencies()
    {
    
    }
}
