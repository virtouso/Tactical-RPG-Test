using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewModelBase : MonoBehaviour
{

    protected abstract void BindDependencies();
    
    protected virtual void Start()
    {
        BindDependencies();
    }

}
