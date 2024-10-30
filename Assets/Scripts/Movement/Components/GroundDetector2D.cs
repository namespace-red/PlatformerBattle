using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundDetector2D : MonoBehaviour
{
    public event Action Grounded;
    
    public bool IsGrounding { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IsGrounding = true;
        Grounded?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsGrounding = false;
    }
}