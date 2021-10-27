using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightingUnitMonoBase : MonoBehaviour
{
    public FightingUnitConfigBase Configs;

    [SerializeField] private Animator _animator;

    public abstract IEnumerator PlayAttack();


    public abstract void PlayMoveAnimation();
    public abstract void StopMoveAnimation();




    public abstract IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float speed);


}
