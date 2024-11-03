using System;
using UnityEngine;

public class Money : MonoBehaviour, IPickable
{
    [field:SerializeField] public int Value { get; private set; } = 1;
    
    public event Action Picked;

    private void OnValidate()
    {
        if (Value < 0)
            Value = 0;
    }
    
    public void PickUp()
    {
        Picked?.Invoke();
        Destroy(gameObject);
    }
}