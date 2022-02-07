using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Input
{

    public class HandHeldTouchInputHandler : InputHandlerBase
    {
        public override Vector3 PointerPosition => throw new NotImplementedException();

        public override Action PointerClicked
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}