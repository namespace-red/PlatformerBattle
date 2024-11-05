using System;

public class IdleState : IState
{
    private IMoveAnimation _moveAnimation;

    public IdleState(IMoveAnimation moveAnimation)
    {
        _moveAnimation = moveAnimation ?? throw new NullReferenceException(nameof(moveAnimation));
    }
    
    public void Enter()
    {
        _moveAnimation.SetMoveState(false);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
    }
}
