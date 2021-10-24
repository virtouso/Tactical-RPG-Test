using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiManager : MonoBehaviour, IMenuUiManager
{

    [SerializeField] private List<PanelKeyObjectPair> _panelsList;
    private Dictionary<PanelKeys, GameObject> _panelsDictionary;
    private Stack<PanelKeys> _panelStack;
    private PanelKeys _currentPanel;




    public void ShowPanel(PanelKeys panelKey)
    {
        _panelsDictionary[_currentPanel].SetActive(false);
        _panelStack.Push(_currentPanel);
        _currentPanel = panelKey;
        _panelsDictionary[panelKey].gameObject.SetActive(true);


    }


    public void ShowPreviousPanel()
    {

        if (_panelStack.Count <= 1) return;
        _panelsDictionary[_currentPanel].SetActive(false);
        PanelKeys prevPanel = _panelStack.Pop();
        _panelsDictionary[prevPanel].SetActive(true);
        _currentPanel = prevPanel;
    }









    #region Utility

    private void InitPanelDictionary()
    {
        _panelsDictionary = new Dictionary<PanelKeys, GameObject>(_panelsList.Count);
        foreach (var item in _panelsList)
        {
            _panelsDictionary.Add(item.PanelKey, item.GameObject);
        }
    }


    private void InitPanelStack()
    {
        _panelStack = new Stack<PanelKeys>();
        _currentPanel = PanelKeys.Root;

    }

    #endregion











    private void Awake()
    {
        InitPanelDictionary();
        InitPanelStack();
    }

}
