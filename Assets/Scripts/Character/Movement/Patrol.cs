using System;
using UnityEngine;

[RequireComponent(typeof(Mover2D))]
public class Patrol : MonoBehaviour
{
    private const float PositionMagnitudeInaccuracy = 0.1f;
    
    [SerializeField] private Transform[] _wayPoints;
    
    private Mover2D _mover;
    private int _currentIndex;

    public Transform Target => _wayPoints[_currentIndex];
    public Vector2 Direction => (Target.position - transform.position).normalized;
    
    private void Awake()
    {
        if (_wayPoints.Length == 0)
            throw new NullReferenceException(name +  " WayPoints is empty");

        _mover = GetComponent<Mover2D>();
    }  
    
    private void FixedUpdate()
    {
        if ((Target.position - transform.position).magnitude < PositionMagnitudeInaccuracy)
        {
            SetNextPoint();
        }

        _mover.Move(Direction);
    }

    private void SetNextPoint()
    {
        _currentIndex = ++_currentIndex % _wayPoints.Length;
    }
}
