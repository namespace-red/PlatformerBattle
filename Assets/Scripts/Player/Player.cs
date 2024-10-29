using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMover2D))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    
    [SerializeField] private PlayerAnimationsController _animations;
    
    private PhysicsMover2D _mover;
    private float _horizontalDirection;
    private bool _isJumped;

    private void Awake()
    {
        if (_animations == null) throw new NullReferenceException(nameof(_animations));
        
        _mover = GetComponent<PhysicsMover2D>();
    }

    private void Update()
    {
        _horizontalDirection = Input.GetAxis(Horizontal);
        
        if (Input.GetButtonDown(Jump))
            _isJumped = true;
    }
    
    private void FixedUpdate()
    {
        _animations.SetMoveSpeed(Math.Abs(_horizontalDirection));
        
        if (_horizontalDirection != 0f)
        {
            _mover.Move(_horizontalDirection);
        }
        
        if (_isJumped)
        {
            _mover.Jump();
            _isJumped = false;
        }
    }

}
