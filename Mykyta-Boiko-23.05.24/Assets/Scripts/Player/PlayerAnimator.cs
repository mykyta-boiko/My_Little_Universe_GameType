using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _movementSpeedAnimationKey;
    [SerializeField] private string _attackAnimationKey;

    private Action _onAttack;

    public void StartAttack()
    {
        _animator.SetBool(_attackAnimationKey, true);
    }

    public void StopAttack()
    {
        _animator.SetBool(_attackAnimationKey, false);
    }    

    public void StartMove(float speed)
    {
        _animator.SetFloat(_movementSpeedAnimationKey, speed);
    }
    public void StopMove()
    {
        _animator.SetFloat(_movementSpeedAnimationKey, 0);
    }
}
