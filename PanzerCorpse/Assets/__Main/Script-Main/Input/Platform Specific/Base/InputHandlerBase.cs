using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Panzers.Input
{
    public abstract class InputHandlerBase : MonoBehaviour
    {
        public abstract Vector3 PointerPosition { get; }
        public abstract System.Action PointerClicked { get; set; }


    }
}