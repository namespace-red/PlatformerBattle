using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damage = 1f;

    public event Action Attacked;

    public float Damage => _damage;

    public void Attack(Health health)
    {
        health.ApplyDamage(_damage);
        Attacked?.Invoke();
    }
}
