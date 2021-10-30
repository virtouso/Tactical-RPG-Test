using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthBarViewModel
{
    public void Init(Model<int> currentHealth, Transform referenceEntity);
}