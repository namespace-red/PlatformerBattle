using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class EnemyCollisionDetector : MonoBehaviour
{
    private const float YNormalAttack = 0f;

    private Attacker _attacker;
    
    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            ContactPoint2D contactPoint = other.contacts.First();

            if (contactPoint.normal.y > YNormalAttack)
                return;
            
            _attacker.Attack(player.Health);
        }
    }
}
