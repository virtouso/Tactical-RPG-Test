using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthBarViewModel
{
    public abstract void Init(Model<int> currentHealth, Transform referenceEntity,Transform parent);
}