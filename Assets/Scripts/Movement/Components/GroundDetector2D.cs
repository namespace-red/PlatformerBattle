using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundDetector2D : MonoBehaviour
{
    [SerializeField]private int _count;
    public event Action Grounded;
    
    public bool IsGrounding { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        
        IsGrounding = true;

        if (++_count == 1)
            Grounded?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger)
            return;

        if (--_count == 0)
            IsGrounding = false;
        
        else if (_count < 0)
            throw new IndexOutOfRangeException(nameof(_count));
    }
}