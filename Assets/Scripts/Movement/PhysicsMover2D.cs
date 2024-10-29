using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMover2D : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForse = 500f;
    [SerializeField] private bool _canControlledInAir;
    [SerializeField] private GroundChecker _groundChecker;

    public event Action Jumping;
    public event Action Falling;
    
    public Rigidbody2D Rigidbody2D { get; private set; }
    
    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        CheckFall();
    }

    public void Move(float horizontalDirection)
    {
        if ((_groundChecker.IsGrounding == false) && (_canControlledInAir == false))
            return;
        
        Rotate(horizontalDirection);
        Rigidbody2D.velocity = new Vector2(horizontalDirection * _speed, Rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        if (_groundChecker.IsGrounding)
        {
            Rigidbody2D.AddForce(_jumpForse * Vector2.up);
            Jumping?.Invoke();
        }
    }

    protected void Init()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
        if (_groundChecker == null) 
            throw new NullReferenceException(nameof(_groundChecker));
    }

    protected void CheckFall()
    {
        if ((_groundChecker.IsGrounding == false) && (Rigidbody2D.velocity.y < 0))
            Falling?.Invoke();
    }

    private void Rotate(float horizontalDirection)
    {
        transform.right = new Vector3(horizontalDirection , 0 , 0);
    }
}
