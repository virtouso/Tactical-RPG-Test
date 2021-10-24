using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupBase : MonoBehaviour
{

    public abstract void Show(string title, string body, System.Action onCloseAction);



}
