using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMover2D))]
public class PhysicsPatrol : MonoBehaviour
{
    private const float PositionInaccuracy = 0.1f;
    
    [SerializeField] private Transform[] _wayPoints;

    private PhysicsMover2D _mover;
    private int _currentIndex;

    public Transform Target => _wayPoints[_currentIndex];
    public float HorizontalDirection => MathF.Sign(Target.position.x - transform.position.x);

    private void Awake()
    {
        if (_wayPoints.Length == 0)
            throw new NullReferenceException(name +  " WayPoints is empty");
        
        _mover = GetComponent<PhysicsMover2D>();
    }
  
    private void FixedUpdate()
    {
        if (Mathf.Abs(Target.position.x - transform.position.x) < PositionInaccuracy)
        {
            SetNextPoint();
        }

        _mover.Move(HorizontalDirection);
    }

    private void SetNextPoint()
    {
        _currentIndex = ++_currentIndex % _wayPoints.Length;
    }
}
