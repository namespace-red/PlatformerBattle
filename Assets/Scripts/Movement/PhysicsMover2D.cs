using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMover2D : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForse = 500f;
    [SerializeField] private bool _canControlledInAir;
    [SerializeField] private GroundChecker _groundChecker;
    
    private Rigidbody2D _rigidbody2D;

    public event Action Jumping;
    public event Action Falling;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        if (_groundChecker == null) throw new NullReferenceException(nameof(_groundChecker));
    }

    private void FixedUpdate()
    {
        if ((_groundChecker.IsGrounding == false) && (_rigidbody2D.velocity.y < 0))
            Falling?.Invoke();
    }

    public void Move(float horizontalDirection)
    {
        if ((_groundChecker.IsGrounding == false) && (_canControlledInAir == false))
            return;

        _rigidbody2D.velocity = new Vector2(horizontalDirection * _speed, _rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        if (_groundChecker.IsGrounding)
        {
            _rigidbody2D.AddForce(_jumpForse * Vector2.up);
            Jumping?.Invoke();
        }
    }
}
