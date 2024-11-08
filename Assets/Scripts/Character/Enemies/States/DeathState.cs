using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class DeathState : IState
{
    private readonly Collider2D _collider2D;
    private readonly IDamageableAnimation _animation;
    private readonly float _secUntilDeath;

    public DeathState(Collider2D collider2D, IDamageableAnimation animation, float secUntilDeath)
    {
        _collider2D = collider2D ?? throw new NullReferenceException(nameof(collider2D));
        _animation = animation ?? throw new NullReferenceException(nameof(animation));
        _secUntilDeath = secUntilDeath > 0f ? secUntilDeath : throw new ArgumentOutOfRangeException(nameof(secUntilDeath));
    }

    public void Enter()
    {
        _collider2D.enabled = false;
        
        var rigidbody = _collider2D.attachedRigidbody;
        
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
            rigidbody.gravityScale = 1f;
            rigidbody.freezeRotation = false;
        }
        
        _animation.PlayDeath();
        
        Object.Destroy(_collider2D.gameObject, _secUntilDeath);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
    }
}
