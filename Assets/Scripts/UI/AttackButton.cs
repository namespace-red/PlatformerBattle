using System;
using UnityEngine;

public class AttackButton : ButtonHandler
{
    [SerializeField] private Health _health;
    
    protected override void Awake()
    {
        if (_health == null) 
            throw new NullReferenceException(nameof(Health));

        base.Awake();
    }

    protected override void OnButtonClicked()
    {
        _health.ApplyDamage(1);
    }
}
