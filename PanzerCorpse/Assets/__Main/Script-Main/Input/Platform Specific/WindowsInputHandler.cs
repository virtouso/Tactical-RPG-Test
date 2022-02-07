using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Panzers.Input
{
    public class WindowsInputHandler : InputHandlerBase
    {
        public override Vector3 PointerPosition => UnityEngine.Input.mousePosition;
        public override Action PointerClicked { get; set; }


        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                PointerClicked?.Invoke();
        }
    }
}