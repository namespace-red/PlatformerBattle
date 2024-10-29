using UnityEngine;

public class HorizontalMoverByPoints : HorizontalMoverByTarget
{
    private const float PositionInaccuracy = 0.1f;
    
    [SerializeField] private Transform[] _wayPoints;
    
    private int _currentIndex;
    
    private void Awake()
    {
        Init();
        Target = _wayPoints[_currentIndex];
    }
    
    private void FixedUpdate()
    {
        if (Mathf.Abs(Target.position.x - transform.position.x) < PositionInaccuracy)
        {
            SetNextPoint();
            Target = _wayPoints[_currentIndex];
        }

        CheckFall();
        Move(HorizontalDirection);
    }

    private void SetNextPoint()
    {
        _currentIndex = ++_currentIndex % _wayPoints.Length;
    }
}
