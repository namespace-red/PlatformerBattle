using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Pusher))]
public class EnemyCollisionDetector : MonoBehaviour
{
    private const float YNormalAttack = 0f;

    public bool CanAttackEnemies;

    private Attacker _attacker;
    private Pusher _pusher;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _pusher = GetComponent<Pusher>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.contacts.First();
        
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (CanAttack(contactPoint))
                Attack(player.Health);
            
            if (player.TryGetComponent(out Rigidbody2D rigidbody2D))
                _pusher.Push(rigidbody2D, -contactPoint.normal);
        }
        else if (CanAttackEnemies && other.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (enemy.TryGetComponent(out Health health))
                if (CanAttack(contactPoint))
                    Attack(health);
            
            if (enemy.TryGetComponent(out Rigidbody2D rigidbody2D))
                _pusher.Push(rigidbody2D, -contactPoint.normal);
        }
    }

    private bool CanAttack(ContactPoint2D contactPoint)
    {
        return contactPoint.normal.y >= YNormalAttack;
    }
    
    private void Attack(Health health)
    {
        _attacker.Attack(health);
    }
}
