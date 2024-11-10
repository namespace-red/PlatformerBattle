using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMover2D))]
[RequireComponent(typeof(HorizontalRotater2D))]
[RequireComponent(typeof(Jumper2D))]
[RequireComponent(typeof(FallDetector2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerCollisionDetector))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimationsController _animationsController;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundDetector2D _groundDetector;
    [SerializeField] private bool _canControlledInAir;

    private PhysicsMover2D _mover;
    private HorizontalRotater2D _rotater;
    private Jumper2D _jumper;
    private FallDetector2D _fallDetector;
    private bool _isJumped;
    
    public Health Health { get; private set; }
    public Attacker Attacker { get; private set; }
    public Wallet Wallet { get; private set; }

    private void Awake()
    {
        if (_animationsController == null) 
            throw new NullReferenceException(nameof(_animationsController));
        
        if (_userInput == null) 
            throw new NullReferenceException(nameof(_userInput));
        
        if (_groundDetector == null) 
            throw new NullReferenceException(nameof(_groundDetector));
        
        _mover = GetComponent<PhysicsMover2D>();
        _rotater = GetComponent<HorizontalRotater2D>();
        _jumper = GetComponent<Jumper2D>();
        _fallDetector = GetComponent<FallDetector2D>();
        
        Health = GetComponent<Health>();
        Attacker = GetComponent<Attacker>();
        Wallet = GetComponent<Wallet>();
    }
    
    private void OnEnable()
    {
        _userInput.JumpPressed += OnJumpPressed;
        _jumper.Jumping += _animationsController.PlayJump;
        _fallDetector.Falling += _animationsController.PlayFall;
        _groundDetector.Grounded += _animationsController.OnGrounded;
        Health.ValueDecreased += OnHeathDecreased;
        Health.Died += OnDied;
    }
    
    private void OnDisable()
    {
        _userInput.JumpPressed -= OnJumpPressed;
        _jumper.Jumping -= _animationsController.PlayJump;
        _fallDetector.Falling -= _animationsController.PlayFall;
        _groundDetector.Grounded -= _animationsController.OnGrounded;
        Health.ValueDecreased -= OnHeathDecreased;
        Health.Died -= OnDied;
    }

    private void FixedUpdate()
    {
        if (_canControlledInAir || _groundDetector.IsGrounding)
        {
            _mover.Move(_userInput.HorizontalInput);
        }
        
        if (_userInput.HorizontalInput != 0f)
        {
            _rotater.Rotate(_userInput.HorizontalInput);
        }

        _animationsController.SetMoveState(_mover.HorizontalVelocity != 0);
        
        if (_isJumped && _groundDetector.IsGrounding)
        {
            _jumper.Jump();
        }
        
        _isJumped = false;
    }
    
    private void OnJumpPressed()
    {
        _isJumped = true;
    }

    private void OnHeathDecreased()
    {
        _animationsController.PlayTakeHit();
    }

    private void OnDied()
    {
        _animationsController.PlayDeath();
        enabled = false;
    }
}
