using System;
using UnityEngine;

[RequireComponent(typeof(HorizontalMoverByPoints))]
public class Mushroom : MonoBehaviour
{
    [SerializeField] private MushroomAnimationsController _animations;
    
    private HorizontalMoverByPoints _mover;

    private void Awake()
    {
        if (_animations == null) 
            throw new NullReferenceException(nameof(_animations));
        
        _mover = GetComponent<HorizontalMoverByPoints>();
    }

    private void FixedUpdate()
    {
        _animations.SetMoveSpeed(Math.Abs(_mover.HorizontalDirection));
    }
}
