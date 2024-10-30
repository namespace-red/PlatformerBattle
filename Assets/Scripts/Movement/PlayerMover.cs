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
    
    [SerializeField] private GroundDetector2D _groundDetector;
    [SerializeField] private bool _canControlledInAir;
    
    private float _horizontalDirection;
    private bool _isJumped;
    private HorizontalRotater2D _rotater;
    
    public HorizontalPhysicsMover2D Mover { get; private set; }
    public Jumper2D Jumper { get; private set; }
    public GroundDetector2D GroundDetector => _groundDetector;
    public FallDetector2D FallDetector { get; private set; }

    private void Awake()
    {
        if (_groundDetector == null) 
            throw new NullReferenceException(nameof(_groundDetector));
        
        Mover = GetComponent<HorizontalPhysicsMover2D>();
        _rotater = GetComponent<HorizontalRotater2D>();
        Jumper = GetComponent<Jumper2D>();
        FallDetector = GetComponent<FallDetector2D>();
    }
    
    private void Update()
    {
        _horizontalDirection = Input.GetAxis(Horizontal);
        
        if (Input.GetButtonDown(Jump))
            _isJumped = true;
    }

    private void FixedUpdate()
    {
        if ((_horizontalDirection != 0f) && 
            (_groundDetector.IsGrounding || _canControlledInAir))
        {
            _rotater.Rotate(_horizontalDirection);
            Mover.Move(_horizontalDirection);
        }
        
        if (_isJumped && _groundDetector.IsGrounding)
        {
            Jumper.Jump();
        }
        
        _isJumped = false;
    }
}
