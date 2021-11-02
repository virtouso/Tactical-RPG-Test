using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : TowerBase
{
    public override void Init(Vector3 position, FieldCoordinate coordinate, TowerCurrentStats initStats)
    {
        transform.position = position;
        _fieldCoordinate = coordinate;
        _towerCurrentStats = initStats;
    }

    public Vector3 Position => transform.position;

    private FieldCoordinate _fieldCoordinate;
    public FieldCoordinate FieldCoordinate => FieldCoordinate;
    
    
    private TowerCurrentStats _towerCurrentStats;
    public TowerCurrentStats TowerCurrentStats
    {
        get => _towerCurrentStats;
        set => _towerCurrentStats = value;
    }
    
    
    
    private void Start()
    {
        StartCoroutine(StartDelayed());
    }

    private IEnumerator StartDelayed()
    {
        yield return new  WaitForEndOfFrame();
        TowerCurrentStats.Health.Action += OnGetDamage;

    }
    
}