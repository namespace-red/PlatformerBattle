using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class EnemyCollisionDetector : MonoBehaviour
{
    private const float YNormalAttack = 0f;

    public bool DoesAttackEnemies;

    private Attacker _attacker;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (CanAttack(other))
                Attack(player.Health);
        }
        else if (DoesAttackEnemies && other.gameObject.TryGetComponent(out Health health))
        {
            if (CanAttack(other))
                Attack(health);
        }
    }

    private bool CanAttack(Collision2D other)
    {
        ContactPoint2D contactPoint = other.contacts.First();

        return contactPoint.normal.y <= YNormalAttack;
    }
    
    private void Attack(Health health)
    {
        _attacker.Attack(health);
    }
}
