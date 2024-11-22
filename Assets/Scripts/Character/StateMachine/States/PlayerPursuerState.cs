using System;
using UnityEngine;

public class PlayerPursuerState : IState
{
    private IMoveAnimation _moveAnimation;
    private TargetPursuer _targetPursuer;
    private HorizontalRotater2D _rotater;
    private float _playerDetectorRadius;

    public PlayerPursuerState(IMoveAnimation moveAnimation, TargetPursuer targetPursuer, HorizontalRotater2D rotater,
        float playerDetectorRadius)
    {
        _moveAnimation = moveAnimation ?? throw new NullReferenceException(nameof(moveAnimation));
        _targetPursuer = targetPursuer ?? throw new NullReferenceException(nameof(targetPursuer));
        _rotater = rotater ?? throw new NullReferenceException(nameof(rotater));
        _playerDetectorRadius = playerDetectorRadius;
    }

    public void Enter()
    {
        foreach (var raycastHit2D in Physics2D.CircleCastAll(_targetPursuer.transform.position, 
            _playerDetectorRadius, Vector2.zero))
        {
            if ((raycastHit2D.rigidbody != null) && (raycastHit2D.rigidbody.TryGetComponent(out Player player)))
            {
                _targetPursuer.Target = player.transform;
                break;
            }
        }
        
        _targetPursuer.enabled = true;
        _moveAnimation.SetMoveState(true);
    }

    public void Exit()
    {
        _moveAnimation.SetMoveState(false);
        _targetPursuer.enabled = false;
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        _rotater.Rotate(_targetPursuer.Direction.x);
    }
}
