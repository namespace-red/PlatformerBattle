using System.Timers;
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
    private Timer _timer = new Timer();

    public float Radius => _radius;
    public Vector3 Center => transform.position + _offset;

    protected override void Awake()
    {
        base.Awake();
        
        _health = GetComponent<Health>();
        _timer.AutoReset = false;
    }

    protected override void AbilityLogic()
    {
        if (_timer.Enabled)
            return;
        
        foreach (var collider in Physics2D.OverlapCircleAll(Center, _radius, _targetLayer))
        {
            if (collider.TryGetComponent(out Health targetHealth))
            {
                var healthValue = Mathf.Min(targetHealth.Value, _healthForTick);
                
                targetHealth.ApplyDamage(healthValue);
                _health.Heal(healthValue);

                _timer.Interval = _tickSec * MsecInSec;
                _timer.Start();
                break;
            }
        }

    }
}
