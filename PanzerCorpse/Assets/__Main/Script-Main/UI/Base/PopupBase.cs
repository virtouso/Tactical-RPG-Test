using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.UI
{
    public abstract class PopupBase : MonoBehaviour
    {

        public abstract void Show(string title, string body, System.Action onCloseAction);

    }
}