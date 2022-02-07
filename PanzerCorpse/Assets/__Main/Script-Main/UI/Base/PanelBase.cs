using System;
using System.Collections;
using System.Collections.Generic;
using Panzers.Manager;
using UnityEngine;
using Zenject;

public abstract class PanelBase : MonoBehaviour
{

    [Inject] protected IMenuUiManager UiManager;

    public abstract void Show(Action<object> action, object data);
    public abstract void Hide();
    
}
