using System.Collections;
using System.Collections.Generic;
using ModestTree;
using PlasticPipe.PlasticProtocol.Client;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBarViewModel : ViewModelBase, IHealthBarViewModel
{
    [SerializeField] private Slider _slider;

    private Camera _camera;
    private Transform _referenceEntityTransform;

    private void UpdateSlider(int value)
    {
        _slider.value = value;
        if(value<=0)
            gameObject.SetActive(false);
    }


    public void Init(Model<int> currentHealth ,Transform referenceEntity,Transform parent)
    {
        gameObject.SetActive(true);
        transform.parent = parent;
        _slider.maxValue = currentHealth.Data;
        _slider.value = currentHealth.Data;
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
    
    
    
    
    
    public class Factory: PlaceholderFactory<HealthBarViewModel>{}
}