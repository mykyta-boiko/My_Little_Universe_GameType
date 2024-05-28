using UnityEngine;
using System;

namespace PlayerLogic
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _movementSpeedAnimationKey;
        [SerializeField] private string _attackAnimationKey;

        private Action _onAttack;

        private void AnimationEventAttack()
        {
            _onAttack?.Invoke();
        }
        public void StartAttack(Action actionOnAttack)
        {
            if (_onAttack == null || _onAttack.GetInvocationList().Length == 0)
            {
                _onAttack += actionOnAttack;
            }
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
}