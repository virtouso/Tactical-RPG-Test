using System.Collections;
using System.Collections.Generic;
using Panzers.Reference;
using UnityEngine;


namespace Panzers.Entities
{
    public class GamePlayCamera : MonoBehaviour, IGamePlayCamera
    {
        [SerializeField] private Animator _animator;

        public IEnumerator PlayStartAnimation()
        {
            _animator.Play(GamePlayCameraAnimatorKeys.StartAnimation);
            yield return new WaitForSeconds(GamePlayCameraAnimatorKeys.StartAnimationTime);
        }
    }
}