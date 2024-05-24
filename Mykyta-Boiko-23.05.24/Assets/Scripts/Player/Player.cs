using IslandLogic;
using UnityEngine;
using ResourceLogic;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [Header("Character Components")]
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Rigidbody _rigidbody;

    [Space(5)] [Header("Character Settigns")]
    [SerializeField] private float _moveSpeed;

    [Space(5)] [Header("Level Components")]
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private ResourceController _resourceContorller;

    private List<GameObject> _targetsToAttack;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float pointX = _joystick.Horizontal * _moveSpeed;
        float pointY = _rigidbody.velocity.y;
        float pointZ = _joystick.Vertical * _moveSpeed;
        _rigidbody.velocity = new Vector3(pointX, pointY, pointZ);

        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.StartMove(_rigidbody.velocity.magnitude);
        }
        else
        {
            _animator.StopMove();
        }

    }
    public void AddTarget(GameObject target)
    {
        _targetsToAttack.Add(target);
        _animator.StartAttack();
    }

    public void RemoveTarget(GameObject target)
    {
        _targetsToAttack.Remove(target);
        if(_targetsToAttack.Count == 0)
        {
            _animator.StopAttack();
        }
    }

    public void TakeResourse(IslandTypes resourse)
    {

    }

    public bool BuildIsland(ResourceType type)
    {
        return _resourceContorller.WasteResourse(type);
    }
}
