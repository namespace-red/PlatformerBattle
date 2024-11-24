using System;
using UnityEngine;

public class TargetPositionMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void Awake()
    {
        if (_target == null) 
            throw new NullReferenceException(nameof(_target));
    }

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
