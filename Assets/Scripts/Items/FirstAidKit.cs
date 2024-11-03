using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FirstAidKit : MonoBehaviour, IPickable
{
    [field:SerializeField] public float HealthValue { get; private set; } = 1;

    public event Action Picked;
    
    private void OnValidate()
    {
        if (HealthValue < 0)
            HealthValue = 0;
    }
    
    public void PickUp()
    {
        Picked?.Invoke();
        Destroy(gameObject);
    }
}
