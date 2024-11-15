using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundDetector2D : MonoBehaviour
{
    private const int OneGround = 1;
    private const int ZeroGround = 0;
    
    [SerializeField] private int _count;
    public event Action Grounded;
    
    public bool IsGrounding { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        
        IsGrounding = true;

        if (++_count == OneGround)
            Grounded?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger)
            return;

        if (--_count == ZeroGround)
            IsGrounding = false;
        
        else if (_count < ZeroGround)
            throw new IndexOutOfRangeException(nameof(_count));
    }
}