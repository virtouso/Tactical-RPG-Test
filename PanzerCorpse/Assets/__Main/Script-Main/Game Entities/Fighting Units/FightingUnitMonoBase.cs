using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightingUnitMonoBase : MonoBehaviour
{
    public FightingUnitPerLevelStats InitialStats;
    public FightingUnitCurrentStats CurrentState;
    [SerializeField] private MeshRenderer _body;

    public FieldCoordinate FieldCoordinate;


    [SerializeField] private Animator _animator;


    public void Init(FightingUnitPerLevelStats config,
        FieldCoordinate coordinate, Vector3 position, Material bodyMaterial, Vector3 lookDirection)
    {
        InitialStats = config;
        CurrentState = new FightingUnitCurrentStats(new Model<int>(config.DamageAmount),
            new Model<int>(config.HealthAmount),
            new Model<int>(config.MovingUnitsInTurn));
        FieldCoordinate = coordinate;
        transform.position = position;
        _body.material = bodyMaterial;
        transform.rotation = Quaternion.LookRotation(lookDirection,Vector3.up);
    }


    public abstract IEnumerator PlayAttack();


    public abstract void PlayMoveAnimation();
    public abstract void StopMoveAnimation();


    public abstract IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float speed);
}