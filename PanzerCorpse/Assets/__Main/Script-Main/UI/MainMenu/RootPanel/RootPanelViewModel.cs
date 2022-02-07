using System.Collections;
using System.Collections.Generic;
using Mvvm;
using UnityEngine;
using UnityEngine.UI;

namespace Panzers.UI
{
    public class RootPanelViewModel : ViewModelBase
    {

        [SerializeField] private RootPanel _rootPanel;

        [SerializeField] private Button _singlePlayerButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;


        protected override void BindDependencies()
        {
            _singlePlayerButton.onClick.AddListener(_rootPanel.SinglePlayerButtonPressed);
            _settingsButton.onClick.AddListener(_rootPanel.SettingButtonPressed);
            _exitButton.onClick.AddListener(_rootPanel.ExitButtonPressed);
        }
    }
}