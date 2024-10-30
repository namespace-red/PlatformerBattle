using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MushroomAnimationsController : MonoBehaviour
{
    [SerializeField] private Mushroom _mushroom;
    
    private Animator _animator;
    private HorizontalMoverByPoints _mover;

    private void Awake()
    {
        if (_mushroom == null) 
            throw new NullReferenceException(nameof(_mushroom));
        
        _animator = GetComponent<Animator>();
        _mover = _mushroom.GetComponent<HorizontalMoverByPoints>();
    }

    private void FixedUpdate()
    {
        SetMoveSpeed(Math.Abs(_mover.HorizontalVelocity));
    }

    private void SetMoveSpeed(float speed) 
        => _animator.SetFloat(Params.Speed, speed);

    private static class Params
    {
        public const string Speed = nameof(Speed);
    }
}
