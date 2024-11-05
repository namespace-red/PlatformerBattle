using System;

public class PatrolState : IState
{
    private IMoveAnimation _moveAnimation;
    private Patrol _patrol;
    private HorizontalRotater2D _rotater;
    
    public PatrolState(IMoveAnimation moveAnimation, Patrol patrol, HorizontalRotater2D rotater)
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
        _rotater.Rotate(_patrol.Direction.x);
    }
}
