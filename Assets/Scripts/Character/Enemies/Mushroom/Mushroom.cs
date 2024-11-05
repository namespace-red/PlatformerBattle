using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsPatrol))]
[RequireComponent(typeof(HorizontalRotater2D))]
[RequireComponent(typeof(Health))]
public class Mushroom : MonoBehaviour
{
    [SerializeField] private MushroomAnimationsController _animationsController;

    private PhysicsPatrol _patrol;
    private HorizontalRotater2D _rotater;
    
    public Health Health { get; private set; }

    private void Awake()
    {
        if (_animationsController == null)
            throw new NullReferenceException(nameof(_animationsController));
        
        _patrol = GetComponent<PhysicsPatrol>();
        _rotater = GetComponent<HorizontalRotater2D>();
        Health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        _rotater.Rotate(_patrol.HorizontalDirection);
        _animationsController.SetMoveState(_patrol.HorizontalDirection != 0);
    }
}
