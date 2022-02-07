using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.DataModel
{
    public class GameState
    {
        public string SelectedMap;

        public GameState(string selectedMap)
        {
            SelectedMap = selectedMap;
        }
    }
}