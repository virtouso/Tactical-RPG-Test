using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsInputHandler : InputHandlerBase
{
    
    public override Vector3 PointerPosition => Input.mousePosition;
    public override Action PointerClicked { get; set; }
  


    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
            PointerClicked?.Invoke();
    }
}