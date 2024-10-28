using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private PhysicsMover2D _mover;
    [SerializeField] private GroundChecker _groundChecker;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        if (_mover == null) throw new NullReferenceException(nameof(_mover));
        if (_groundChecker == null) throw new NullReferenceException(nameof(_groundChecker));
    }

    private void OnEnable()
    {
        _mover.Jumping += OnJumping;
        _mover.Falling += OnFalling;
        _groundChecker.Grounded += OnGrounded;
        // _attacker.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _mover.Jumping += OnJumping;
        _mover.Falling += OnFalling;
        _groundChecker.Grounded += OnGrounded;
        // _attacker.Attacked -= OnAttacked;
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
    
    public void SetMoveSpeed(float speed) 
        => _animator.SetFloat(Params.Speed, speed);
    
    private static class Params
    {
        public const string Speed = nameof(Speed);
        public const string IsJumping = nameof(IsJumping);
        public const string IsFalling = nameof(IsFalling);
    }
}
