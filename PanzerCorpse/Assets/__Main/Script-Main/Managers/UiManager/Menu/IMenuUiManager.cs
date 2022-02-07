using System.Collections;
using System.Collections.Generic;
using Panzers.Reference;
using UnityEngine;

namespace Panzers.Manager
{
    public interface IMenuUiManager
    {


        void ShowPanel(PanelKeys panelKey, System.Action<object> action = null, object data = null);
        public void ShowPreviousPanel();

    }
}






