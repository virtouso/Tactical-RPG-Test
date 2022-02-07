using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mvvm
{
    public abstract class ViewModelBase : MonoBehaviour
    {

        protected abstract void BindDependencies();

        protected virtual void Start()
        {
            BindDependencies();
        }

    }
}