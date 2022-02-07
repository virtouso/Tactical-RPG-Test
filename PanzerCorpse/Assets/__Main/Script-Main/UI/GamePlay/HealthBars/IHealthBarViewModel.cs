using System.Collections;
using System.Collections.Generic;
using Mvvm;
using UnityEngine;

namespace Panzers.UI
{
    public interface IHealthBarViewModel
    {
        public abstract void Init(Model<int> currentHealth, Transform referenceEntity, Transform parent);
    }
}