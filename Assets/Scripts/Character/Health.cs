using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxValue;
    [SerializeField] private float _value;
    
    public event Action<float, float> ValueChanged;
    public event Action ValueDecreased;
    public event Action ValueIncreased;
    public event Action Died;
    
    private float Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, 0 , MaxValue);
            ValueChanged?.Invoke(Value, MaxValue);
            
            if (Value == 0)
                Died?.Invoke();
        }
    }

    public bool IsAlive => Value > 0;
    public bool CanBeHealed => Value < MaxValue;
    
    public void ApplyDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        Value -= damage;
        ValueDecreased?.Invoke();
    }

    public void Heal(float value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();

        Value += value;
        ValueIncreased?.Invoke();
    }
    
    public void HealFull()
    {
        if (Value < MaxValue)
        {
            Value = MaxValue;
            ValueIncreased?.Invoke();
        }
    }
}
