using UnityEngine;

public class HorizontalMoverByPoints : HorizontalMoverByTarget
{
    private const float PositionInaccuracy = 0.1f;
        
    [SerializeField] private Transform _pointParent;
    
    private Transform[] _points;
    private int _currentIndex;
    
    private void Awake()
    {
        Init();
        InitPoints();
        Target = _points[_currentIndex];
    }
    
    private void FixedUpdate()
    {
        if (Mathf.Abs(Target.position.x - transform.position.x) < PositionInaccuracy)
        {
            SetNextPoint();
            Target = _points[_currentIndex];
        }

        CheckFall();
        Move(HorizontalDirection);
    }

    private void InitPoints()
    {
        _points = new Transform[_pointParent.childCount];

        for (int i = 0; i < _pointParent.childCount; i++)
            _points[i] = _pointParent.GetChild(i).transform;
    }

    private void SetNextPoint()
    {
        _currentIndex++;

        if (_currentIndex == _points.Length)
            _currentIndex = 0;
    }
}
