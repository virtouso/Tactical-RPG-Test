using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightingUnitMonoBase : MonoBehaviour
{
    public FightingUnitPerLevelStats InitialStats;
    public FightingUnitCurrentStats CurrentState;


    public FieldCoordinate FieldCoordinate;


    [SerializeField] private Animator _animator;


    public void Init(FightingUnitPerLevelStats config, FightingUnitCurrentStats currentStats,
        FieldCoordinate coordinate, Vector3 position)
    {
        InitialStats = config;
        CurrentState = currentStats;
        FieldCoordinate = coordinate;
        transform.position = position;
    }


    public abstract IEnumerator PlayAttack();


    public abstract void PlayMoveAnimation();
    public abstract void StopMoveAnimation();


    public abstract IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float speed);
}