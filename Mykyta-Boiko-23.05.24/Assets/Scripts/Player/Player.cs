using IslandLogic;
using UnityEngine;
using ResourceLogic;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [Header("Character Components")]
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _axeTool;
    [SerializeField] private GameObject _crutchTool;

    [Space(5)] [Header("Character Settigns")]
    [SerializeField] private float _moveSpeed;

    [Space(5)] [Header("Level Components")]
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private ResourceController _resourceContorller;

    private List<ResourceOnIsland> _targetsToAttack = new List<ResourceOnIsland>();
    private List<ResourceOnIsland> _targetsToRemove = new List<ResourceOnIsland>();
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

    private void Attack()
    {
        foreach (ResourceOnIsland target in _targetsToAttack)
        {
            target.TakeHit();
        }
        CheckCleanupTargets();
    }
    private void CheckCleanupTargets()
    {
        for (int i = _targetsToRemove.Count - 1; i >= 0; i--)
        {
            _targetsToAttack.Remove(_targetsToRemove[i]);
            _targetsToRemove.RemoveAt(i); // Safely removing items from a list
        }
        if (_targetsToAttack.Count == 0)
        {
            _axeTool.SetActive(false);
            _crutchTool.SetActive(false);
            _animator.StopAttack();
        }       
    }

    private void ChoiceInstrument(IslandTypes resourceType)
    {
        switch (resourceType)
        {
            case IslandTypes.Wood:
                _axeTool.SetActive(true);
                break;
            case IslandTypes.Stone:
                _crutchTool.SetActive(true);
                break;
            case IslandTypes.Crystal:
                _crutchTool.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void AddTarget(ResourceOnIsland target, IslandTypes resourceType)
    {
        ChoiceInstrument(resourceType);
        if(!_targetsToAttack.Contains(target))
        {
            _targetsToAttack.Add(target);
        }
        _animator.StartAttack(Attack);
    }

    public void AddTargetToRemove(ResourceOnIsland target)
    {
        _targetsToRemove.Add(target);
    }

    public void TakeResourse(IslandTypes resourse)
    {
        _resourceContorller.AddResourse(resourse);
    }

    public bool BuildIsland(ResourceType type)
    {
        return _resourceContorller.WasteResourse(type);
    }
}
