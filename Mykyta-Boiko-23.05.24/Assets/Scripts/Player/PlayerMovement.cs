using UnityEngine;

namespace PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GroundChecker _groundChecker;

        [Space(5)]
        [Header("Settings")]
        [SerializeField] private string _joystickTag = "GameController";
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotateSpeed = 5f;
        [SerializeField] private float _gravityForce = 20f;

        private Vector3 _velocity;

        private void Start()
        {
            if(_joystick == null)
            {
                _joystick = GameObject.FindGameObjectWithTag(_joystickTag).GetComponent<FloatingJoystick>();
            }
        }

        private void FixedUpdate()
        {
            GravityHandling();
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
            float joystickDistance = new Vector2(_joystick.Horizontal, _joystick.Vertical).magnitude;

            if (joystickDistance > 0)
            {
                if (_groundChecker.IsGrounded)
                {
                    float adjustedSpeed = _moveSpeed * joystickDistance;
                    Vector3 movement = moveDirection * adjustedSpeed;
                    movement.y = _velocity.y; 

                    _characterController.Move(movement * Time.deltaTime);

                    Vector3 horizontalMovement = new Vector3(movement.x, 0, movement.z);
                    if (horizontalMovement.magnitude > 0.1f)
                    {
                        _animator.StartMove(horizontalMovement.magnitude);
                    }
                }
                else
                {
                    _animator.StopMove();
                }
                RotateCharacter(moveDirection);
            }
            else
            {
                _animator.StopMove();
            }
        }

        private void RotateCharacter(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed * Time.deltaTime, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }

        private void GravityHandling()
        {
            if (!_characterController.isGrounded)
            {
                _velocity.y = 0f;
            }
            else
            {
                _velocity.y -= _gravityForce * Time.deltaTime;
            }
        }
    }
}