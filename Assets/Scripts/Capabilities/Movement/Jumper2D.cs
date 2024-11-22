using System;
using UnityEngine;

public class Jumper2D : MonoBehaviour
{
    [SerializeField] private float _jumpForse = 500f;
    
    public event Action Jumping;
    
    public Rigidbody2D Rigidbody2D { get; private set; }
    
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        Rigidbody2D.AddForce(_jumpForse * Vector2.up);
        Jumping?.Invoke();
    }
}
