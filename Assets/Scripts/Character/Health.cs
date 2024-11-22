using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxValue;
    
    [SerializeField] private float _value;
    
    public event Action<float> ValueChangedPercent;
    public event Action<float, float> ValueChanged;
    public event Action ValueDecreased;
    public event Action ValueIncreased;
    public event Action Died;
    
    public float Value
    {
        get => _value;
        private set
        {
            _value = Mathf.Clamp(value, 0 , MaxValue);
            ValueChangedPercent?.Invoke(PercentValue);
            ValueChanged?.Invoke(_value, MaxValue);
            
            if (Value == 0)
                Died?.Invoke();
        }
    }

    public float PercentValue => Value / MaxValue;
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
