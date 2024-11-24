using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : AbilityWithCoolDown
{
    [SerializeField, Min(0)] private float _tickSec;
    [SerializeField, Min(0)] private float _healthForTick = 1;
    [SerializeField, Min(0)] private float _radius = 2;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _targetLayer;

    private Health _health;
    private float _tickTimer;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _tickTimer = _tickSec;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _offset, _radius);
    }

    protected override void AbilityLogic()
    {
        _tickTimer += Time.deltaTime;
        
        if (_tickTimer < _tickSec)
            return;
        
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position + _offset, _radius, _targetLayer))
        {
            if (collider.TryGetComponent(out Health targetHealth))
            {
                var healthValue = Mathf.Min(targetHealth.Value, _healthForTick);
                
                targetHealth.ApplyDamage(healthValue);
                _health.Heal(healthValue);
                
                _tickTimer = 0;
                break;
            }
        }

    }
}
