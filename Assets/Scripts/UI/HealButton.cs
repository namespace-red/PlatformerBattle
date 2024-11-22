using System;
using UnityEngine;

public class HealButton : ButtonHandler
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
        _health.Heal(1);
    }
}
