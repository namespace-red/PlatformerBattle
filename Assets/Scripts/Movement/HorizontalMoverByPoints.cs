using System;
using UnityEngine;

public class HorizontalMoverByPoints : HorizontalPhysicsMover2D
{
    private const float PositionInaccuracy = 0.1f;
    
    [SerializeField] private Transform[] _wayPoints;
    
    private int _currentIndex;

    public Transform Target => _wayPoints[_currentIndex];
    public float HorizontalDirection => (Target.position.x - transform.position.x) > 0 ? 1f : -1f;

    private void Awake()
    {
        if (_wayPoints.Length == 0)
            throw new NullReferenceException(name +  " WayPoints is empty");
        
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
  
    private void FixedUpdate()
    {
        if (Mathf.Abs(Target.position.x - transform.position.x) < PositionInaccuracy)
        {
            SetNextPoint();
        }

        Move(HorizontalDirection);
    }

    private void SetNextPoint()
    {
        _currentIndex = ++_currentIndex % _wayPoints.Length;
    }
}
