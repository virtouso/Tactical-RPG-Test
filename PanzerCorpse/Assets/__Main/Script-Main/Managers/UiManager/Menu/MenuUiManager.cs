using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiManager : MonoBehaviour, IMenuUiManager
{
    [SerializeField] private List<PanelKeyPanelBasePair> _panelsList;
    private Dictionary<PanelKeys, PanelBase> _panelsDictionary;
    private Stack<PanelKeys> _panelStack;
    private PanelKeys _currentPanel;


    public void ShowPanel(PanelKeys panelKey, System.Action<object> action = null, object data = null)
    {
        _panelsDictionary[_currentPanel].Hide();
        _panelsDictionary[_currentPanel].gameObject.SetActive(false);
        _panelStack.Push(_currentPanel);

        _currentPanel = panelKey;
        _panelsDictionary[panelKey].gameObject.SetActive(true);
        _panelsDictionary[panelKey].Show(action, data);
    }


    public void ShowPreviousPanel()
    {
        if (_panelStack.Count <= 1) return;
        _panelsDictionary[_currentPanel].gameObject.SetActive(false);
        PanelKeys prevPanel = _panelStack.Pop();
        _panelsDictionary[prevPanel].gameObject.SetActive(true);
        _currentPanel = prevPanel;
    }


    #region Utility

    private void InitPanelDictionary()
    {
        _panelsDictionary = new Dictionary<PanelKeys, PanelBase>(_panelsList.Count);
        foreach (var item in _panelsList)
        {
            _panelsDictionary.Add(item.PanelKey, item.Panel);
        }
    }


    private void InitPanelStack()
    {
        _panelStack = new Stack<PanelKeys>();
    }

    #endregion


    private void Awake()
    {
        InitPanelDictionary();
        InitPanelStack();
    }


    private void Start()
    {
        ShowPanel(PanelKeys.Root);
    }
}