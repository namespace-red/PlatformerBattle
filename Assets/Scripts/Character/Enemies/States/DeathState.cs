using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class DeathState : IState
{
    private readonly float _throwingForce = 8f;
    private readonly float _gravityScale = 5f;
    private readonly float _secUntilDeath = 1f;
    private readonly float _rotationSpeed = 90f;
    private readonly float _maxAngle = 45f;
    
    private readonly Collider2D _collider2D;
    private readonly IDamageableAnimation _animation;
    private readonly Transform _transform;

    public DeathState(Collider2D collider2D, IDamageableAnimation animation)
    {
        _collider2D = collider2D ?? throw new NullReferenceException(nameof(collider2D));
        _animation = animation ?? throw new NullReferenceException(nameof(animation));
        _transform = _collider2D.transform;
        
        int randomMultiplier = Random.Range(0, 2) * 2 - 1;
        _rotationSpeed *= randomMultiplier;
    }

    public void Enter()
    {
        _collider2D.enabled = false;
        
        var rigidbody = _collider2D.attachedRigidbody;
        
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(Vector2.up * _throwingForce, ForceMode2D.Impulse);
            rigidbody.gravityScale = _gravityScale;
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
        if (Math.Abs(_transform.rotation.z) < _maxAngle)
            _transform.Rotate(Vector3.forward, _rotationSpeed * Time.fixedDeltaTime);
    }
}
