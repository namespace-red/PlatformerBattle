using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallDetector2D : MonoBehaviour
{
    private const int NumberOfFractional = 2;
    
    [SerializeField] private float _minSpeedFall = -0.01f;
    
    private float _pastVelocityY;
    
    public event Action Falling;
    
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float velocityY = MathF.Round(Rigidbody2D.velocity.y, NumberOfFractional);
        
        if ((velocityY <= _minSpeedFall) && (_pastVelocityY > _minSpeedFall))
        {
            Falling?.Invoke();
        }
        
        _pastVelocityY = velocityY;
    }

    private void OnValidate()
    {
        if (_minSpeedFall > 0)
            _minSpeedFall *= -1;
    }
}
