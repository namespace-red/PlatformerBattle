using System;
using UnityEngine;

[RequireComponent(typeof(Mover2D))]
public class TargetPursuer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private Mover2D _mover;

    public Transform Target
    {
        get => _target;
        set
        {
            if (value == null)
                throw new NullReferenceException(nameof(value));
            
            _target = value;
        }
    }

    public Vector2 Direction => (_target.position - transform.position).normalized;
    
    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
    }
    
    private void FixedUpdate()
    {
        _mover.Move(Direction);
    }
}
