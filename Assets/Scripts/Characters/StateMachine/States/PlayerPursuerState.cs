using System;

public class PlayerPursuerState : IState
{
    private IMoveAnimation _moveAnimation;
    private TargetPursuer _targetPursuer;
    private HorizontalRotater2D _rotater;
    private PlayerDetector _playerDetector;

    public PlayerPursuerState(IMoveAnimation moveAnimation, TargetPursuer targetPursuer, HorizontalRotater2D rotater,
        PlayerDetector playerDetector)
    {
        _moveAnimation = moveAnimation ?? throw new NullReferenceException(nameof(moveAnimation));
        _targetPursuer = targetPursuer ?? throw new NullReferenceException(nameof(targetPursuer));
        _rotater = rotater ?? throw new NullReferenceException(nameof(rotater));
        _playerDetector = playerDetector;
    }

    public void Enter()
    {
        _targetPursuer.Target = _playerDetector.Detect().transform;
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
