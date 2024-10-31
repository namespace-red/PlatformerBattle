using System;
using UnityEngine;

[RequireComponent(typeof(HorizontalPhysicsMover2D))]
[RequireComponent(typeof(HorizontalRotater2D))]
[RequireComponent(typeof(Jumper2D))]
[RequireComponent(typeof(FallDetector2D))]
[RequireComponent(typeof(Wallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimationsController _animationsController;
    [SerializeField] private UserInput _userInput;
    [SerializeField] private GroundDetector2D _groundDetector;
    [SerializeField] private bool _canControlledInAir;

    private HorizontalPhysicsMover2D _mover;
    private HorizontalRotater2D _rotater;
    private Jumper2D _jumper;
    private FallDetector2D _fallDetector;
    private Wallet _wallet;
    private bool _isJumped;

    private void Awake()
    {
        if (_animationsController == null) 
            throw new NullReferenceException(nameof(_animationsController));
        
        if (_userInput == null) 
            throw new NullReferenceException(nameof(_userInput));
        
        if (_groundDetector == null) 
            throw new NullReferenceException(nameof(_groundDetector));
        
        _mover = GetComponent<HorizontalPhysicsMover2D>();
        _rotater = GetComponent<HorizontalRotater2D>();
        _jumper = GetComponent<Jumper2D>();
        _fallDetector = GetComponent<FallDetector2D>();
        _wallet = GetComponent<Wallet>();
    }
    
    private void OnEnable()
    {
        _userInput.JumpPressed += OnJumpPressed;
        _jumper.Jumping += _animationsController.PlayJump;
        _fallDetector.Falling += _animationsController.PlayFall;
        _groundDetector.Grounded += _animationsController.OnGrounded;
    }
    
    private void OnDisable()
    {
        _userInput.JumpPressed -= OnJumpPressed;
        _jumper.Jumping -= _animationsController.PlayJump;
        _fallDetector.Falling -= _animationsController.PlayFall;
        _groundDetector.Grounded -= _animationsController.OnGrounded;
    }

    private void FixedUpdate()
    {
        _animationsController.SetRunState(_userInput.HorizontalInput != 0);
        
        if ((_userInput.HorizontalInput != 0f) && 
            (_canControlledInAir || _groundDetector.IsGrounding))
        {
            _rotater.Rotate(_userInput.HorizontalInput);
            _mover.Move(_userInput.HorizontalInput);
        }
        
        if (_isJumped && _groundDetector.IsGrounding)
        {
            _jumper.Jump();
        }
        
        _isJumped = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickable pickated))
        {
            switch (pickated)
            {
                case Money money:
                    _wallet.AddMoney(money.Value);
                    money.PickUp();
                    break;

                default:
                    throw new ArgumentException($"Not correct type {pickated}");
            }
        }
    }

    private void OnJumpPressed()
    {
        _isJumped = true;
    }
}
