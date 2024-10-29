using System;
using UnityEngine;

public class HorizontalMoverByTarget : PhysicsMover2D
{
    [SerializeField] private Transform _target;

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }

    public float HorizontalDirection
    {
        get => (_target.position.x - transform.position.x) > 0 ? 1f : -1f;
    }

    private void Awake()
    {
        Init();
        
        if (_target == null) 
            throw new NullReferenceException(nameof(_target));
    }

    private void FixedUpdate()
    {
        CheckFall();
        Move(HorizontalDirection);
    }
}
