using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayCamera :MonoBehaviour, IGamePlayCamera
{
    [SerializeField] private Animator _animator;

    public IEnumerator PlayStartAnimation()
    {
        _animator.Play(GamePlayCameraAnimatorKeys.StartAnimation);
        yield return new WaitForSeconds(GamePlayCameraAnimatorKeys.StartAnimationTime);
    }
}