using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundChecker : MonoBehaviour
{
    private BoxCollider2D _collider;

    public event Action Grounded;
    
    public bool IsGrounding { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (_collider == null)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_collider.transform.position, _collider.transform.localScale);
    }

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