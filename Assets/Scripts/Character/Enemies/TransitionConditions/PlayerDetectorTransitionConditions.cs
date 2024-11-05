using System.Linq;
using UnityEngine;

public class PlayerDetectorTransitionConditions : ITransitionCondition
{
    private Transform _detector;
    private float _radius;

    public PlayerDetectorTransitionConditions(Transform detector, float radius)
    {
        _detector = detector;
        _radius = radius;
    }
    public bool IsDone()
    {
        return Physics2D.CircleCastAll(_detector.position, _radius, Vector2.zero)
            .Any(raycastHit2D => raycastHit2D.rigidbody.TryGetComponent(out Player player));
    }
}
