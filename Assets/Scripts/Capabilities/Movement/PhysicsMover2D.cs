using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMover2D : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    
    public Rigidbody2D Rigidbody2D { get; protected set; }
    public float HorizontalVelocity => MathF.Round(Rigidbody2D.velocity.x);

    protected void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void Move(float horizontalDirection)
    {
        Rigidbody2D.velocity = new Vector2(horizontalDirection * _speed, Rigidbody2D.velocity.y);
    }
}
