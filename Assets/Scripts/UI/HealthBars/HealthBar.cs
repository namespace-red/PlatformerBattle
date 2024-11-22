using System;
using UnityEngine;

public class HealthBar : SimpleBar
{
    [SerializeField] private Health _health;
    
    protected override void Awake()
    {
        if (_health == null) 
            throw new NullReferenceException(nameof(Health));
        
        base.Awake();
    }
    
    private void OnEnable()
    {
        _health.ValueChangedPercent += SetValue;
        SetValue(_health.PercentValue);
    }

    private void OnDisable()
    {
        _health.ValueChangedPercent -= SetValue;
    }
}
