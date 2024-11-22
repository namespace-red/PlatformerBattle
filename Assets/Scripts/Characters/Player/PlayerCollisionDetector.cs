using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]
public class PlayerCollisionDetector : MonoBehaviour
{
    private const float YNormalAttack = 1f;
    
    private Wallet _wallet;
    private Health _health;
    private Attacker _attacker;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickable pickated))
        {
            switch (pickated)
            {
                case Money money:
                    _wallet.AddMoney(money.Value);
                    money.PickUp();
                    break;
                
                case FirstAidKit firstAidKit:
                    if (_health.CanBeHealed)
                    {
                        _health.Heal(firstAidKit.HealthValue);
                        firstAidKit.PickUp();
                    }

                    break;

                default:
                    throw new ArgumentException($"Not correct type {pickated}");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Health health))
        {
            ContactPoint2D contactPoint = other.contacts.First();

            if (contactPoint.normal.y < YNormalAttack)
                return;
            
            _attacker.Attack(health);
        }
    }
}
