using System.Collections;
using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Client;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarViewModel : ViewModelBase, IHealthBarViewModel
{
    [SerializeField] private Slider _slider;

    private Camera _camera;
    private Transform _referenceEntityTransform;

    private void UpdateSlider(int value)
    {
        _slider.value = value;
    }


    public void Init(Model<int> currentHealth ,Transform referenceEntity)
    {
        _slider.maxValue = currentHealth.Data;
        currentHealth.Action += UpdateSlider;
        _referenceEntityTransform = referenceEntity;
        BindDependencies();
    }

    protected override void BindDependencies()
    {
    }


    protected override void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 healthPosition = _camera.WorldToScreenPoint(_referenceEntityTransform.position);
        _slider.transform.position = healthPosition;
    }
}