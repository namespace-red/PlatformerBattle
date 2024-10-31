using System;
using UnityEngine;

[RequireComponent(typeof(HorizontalMoverByPoints))]
[RequireComponent(typeof(HorizontalRotater2D))]
public class Mushroom : MonoBehaviour
{
    [SerializeField] private MushroomAnimationsController _animationsController;

    private HorizontalMoverByPoints _mover;
    private HorizontalRotater2D _rotater;

    private void Awake()
    {
        if (_animationsController == null)
            throw new NullReferenceException(nameof(_animationsController));
        
        _mover = GetComponent<HorizontalMoverByPoints>();
        _rotater = GetComponent<HorizontalRotater2D>();
    }

    private void FixedUpdate()
    {
        _rotater.Rotate(_mover.HorizontalDirection);
        _animationsController.SetWalkState(_mover.HorizontalVelocity != 0);
    }
}
