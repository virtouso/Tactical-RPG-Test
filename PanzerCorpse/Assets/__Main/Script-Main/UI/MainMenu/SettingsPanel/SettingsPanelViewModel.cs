using System.Collections;
using System.Collections.Generic;
using Mvvm;
using UnityEngine;
using UnityEngine.UI;

namespace Panzers.UI
{
    public class SettingsPanelViewModel : ViewModelBase
    {
        [SerializeField] private SettingsPanel _settingsPanel;

        [SerializeField] private Slider _effectSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Button _backButton;

        protected override void BindDependencies()
        {

            _effectSlider.onValueChanged.AddListener(_settingsPanel.UpdateAfx);
            _musicSlider.onValueChanged.AddListener(_settingsPanel.UpdateMusic);
            _backButton.onClick.AddListener(_settingsPanel.BackButtonPressed);

        }

        protected override void Start()
        {
            base.Start();
            _effectSlider.value = _settingsPanel.AfxVolume;
            _musicSlider.value = _settingsPanel.MusicVolume;
        }

    }
}