using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.DataModel
{
    [System.Serializable]
    public class FieldCoordinate
    {
        public int X;
        public int Y;

        public FieldCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}