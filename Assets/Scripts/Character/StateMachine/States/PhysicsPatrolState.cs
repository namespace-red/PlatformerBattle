using System;

public class PhysicsPatrolState : IState
{
    private IMoveAnimation _moveAnimation;
    private PhysicsPatrol _patrol;
    private HorizontalRotater2D _rotater;
    
    public PhysicsPatrolState(IMoveAnimation moveAnimation, PhysicsPatrol patrol, HorizontalRotater2D rotater)
    {
        _moveAnimation = moveAnimation ?? throw new NullReferenceException(nameof(moveAnimation));
        _patrol = patrol ?? throw new NullReferenceException(nameof(patrol));
        _rotater = rotater ?? throw new NullReferenceException(nameof(rotater));
    }

    public void Enter()
    {
        _patrol.enabled = true;
        _moveAnimation.SetMoveState(true);
    }

    public void Exit()
    {
        _moveAnimation.SetMoveState(false);
        _patrol.enabled = false;
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        _rotater.Rotate(_patrol.HorizontalDirection);
    }
}
