using System;
using UnityEngine;

public class SmoothHealthBar : SmoothBar
{
    [SerializeField] private Health _health;

    protected override void Awake()
    {
        if (_health == null) 
            throw new NullReferenceException(nameof(_health));
        
        base.Awake();
    }

    private void OnEnable()
    {
        _health.ValueChangedPercent += SetValueSmoothly;
        SetValue(_health.PercentValue);
    }

    private void OnDisable()
    {
        _health.ValueChangedPercent -= SetValueSmoothly;
    }
}