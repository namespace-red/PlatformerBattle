using System;
using UnityEngine;

public class SmoothHealthBar : SmoothBar
{
    [SerializeField] protected Health Health;

    protected override void Awake()
    {
        if (Health == null) 
            throw new NullReferenceException(nameof(Health));
        
        base.Awake();
    }

    private void OnEnable()
    {
        Health.ValueChangedPercent += SetValueSmoothly;
        SetValue(Health.PercentValue);
    }

    private void OnDisable()
    {
        Health.ValueChangedPercent -= SetValueSmoothly;
    }
}