using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{


    public abstract void Show(Action<object> action, object data);
    protected abstract void Hide();



}
