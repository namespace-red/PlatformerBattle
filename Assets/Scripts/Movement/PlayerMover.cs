using System;
using UnityEngine;

[RequireComponent(typeof(HorizontalPhysicsMover2D))]
[RequireComponent(typeof(HorizontalRotater2D))]
[RequireComponent(typeof(Jumper2D))]
[RequireComponent(typeof(FallDetector2D))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    
    [SerializeField] private PlayerAnimationsController _animationsController;
    [SerializeField] private GroundDetector2D _groundDetector;
    [SerializeField] private bool _canControlledInAir;
    
    private float _horizontalDirection;
    private bool _isJumped;
    private HorizontalPhysicsMover2D _mover;
    private HorizontalRotater2D _rotater;
    private Jumper2D _jumper;
    private FallDetector2D _fallDetector;

    private void Awake()
    {
        if (_animationsController == null) 
            throw new NullReferenceException(nameof(_animationsController));
        
        if (_groundDetector == null) 
            throw new NullReferenceException(nameof(_groundDetector));
        
        _mover = GetComponent<HorizontalPhysicsMover2D>();
        _rotater = GetComponent<HorizontalRotater2D>();
        _jumper = GetComponent<Jumper2D>();
        _fallDetector = GetComponent<FallDetector2D>();
    }

    private void OnEnable()
    {
        _jumper.Jumping += _animationsController.PlayJump;
        _fallDetector.Falling += _animationsController.PlayFall;
        _groundDetector.Grounded += _animationsController.OnGrounded;
    }
    
    private void OnDisable()
    {
        _jumper.Jumping -= _animationsController.PlayJump;
        _fallDetector.Falling -= _animationsController.PlayFall;
        _groundDetector.Grounded -= _animationsController.OnGrounded;
    }

    private void Update()
    {
        _horizontalDirection = Input.GetAxis(Horizontal);
        
        if (Input.GetButtonDown(Jump))
            _isJumped = true;
    }

    private void FixedUpdate()
    {
        _animationsController.SetRunState(_horizontalDirection != 0);
        
        if ((_horizontalDirection != 0f) && 
            (_canControlledInAir || _groundDetector.IsGrounding))
        {
            _rotater.Rotate(_horizontalDirection);
            _mover.Move(_horizontalDirection);
        }
        
        if (_isJumped && _groundDetector.IsGrounding)
        {
            _jumper.Jump();
        }
        
        _isJumped = false;
    }
}
