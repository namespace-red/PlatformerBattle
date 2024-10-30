using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    
    private Animator _animator;
    private HorizontalPhysicsMover2D _mover;
    private Jumper2D _jumper;
    private GroundDetector2D _groundDetector;
    private FallDetector2D _fallDetector;

    private void Awake()
    {
        if (_playerMover == null) 
            throw new NullReferenceException(nameof(_playerMover));
        
        _animator = GetComponent<Animator>();
        
        _mover = _playerMover.Mover;
        _jumper = _playerMover.Jumper;
        _groundDetector = _playerMover.GroundDetector;
        _fallDetector = _playerMover.FallDetector;
    }

    private void OnEnable()
    {
        _jumper.Jumping += OnJumping;
        _fallDetector.Falling += OnFalling;
        _groundDetector.Grounded += OnGrounded;
    }

    private void OnDisable()
    {
        _jumper.Jumping += OnJumping;
        _fallDetector.Falling += OnFalling;
        _groundDetector.Grounded += OnGrounded;
    }

    private void FixedUpdate()
    {
        SetMoveSpeed(Math.Abs(_mover.HorizontalVelocity));
    }

    private void OnJumping() 
        => _animator.SetBool(Params.IsJumping, true);
    
    private void OnFalling()
    {
        _animator.SetBool(Params.IsJumping, false);
        _animator.SetBool(Params.IsFalling, true);
    }
    
    private void OnGrounded()
    {
        _animator.SetBool(Params.IsJumping, false);
        _animator.SetBool(Params.IsFalling, false);
    }
    
    private void SetMoveSpeed(float speed) 
        => _animator.SetFloat(Params.Speed, speed);
    
    private static class Params
    {
        public const string Speed = nameof(Speed);
        public const string IsJumping = nameof(IsJumping);
        public const string IsFalling = nameof(IsFalling);
    }
}
