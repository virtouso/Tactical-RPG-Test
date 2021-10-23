using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModelViewBase : MonoBehaviour
{

    protected abstract void BindDependencies();





    private void Start()
    {
        BindDependencies();
    }

}
