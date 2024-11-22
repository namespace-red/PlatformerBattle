using System;
using UnityEngine;

public class TargetPositionMover : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Awake()
    {
        if (_target == null) 
            throw new NullReferenceException(nameof(_target));
    }

    private void LateUpdate()
    {
        transform.position = _target.position;
    }
}
