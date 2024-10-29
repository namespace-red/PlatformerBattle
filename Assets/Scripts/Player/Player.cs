using System;
using UnityEngine;

[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PhysicsMover2D))]
public class Player : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    
    [SerializeField] private PlayerAnimationsController _animations;
    
    private PhysicsMover2D _mover;
    private Wallet _wallet;
    private float _horizontalDirection;
    private bool _isJumped;

    private void Awake()
    {
        if (_animations == null) 
            throw new NullReferenceException(nameof(_animations));
        
        _mover = GetComponent<PhysicsMover2D>();
        _wallet = GetComponent<Wallet>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Money money))
        {
            _wallet.AddMoney(money.Value);
            money.PickUp();
        }
    }
}
