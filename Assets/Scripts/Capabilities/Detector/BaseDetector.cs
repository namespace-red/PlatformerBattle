using System.Collections.Generic;
using UnityEngine;

public class BaseDetector<T> : MonoBehaviour
    where T : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _targetLayer;

    public Vector3 Center => transform.position + _offset;
    
    public float Radius
    {
        get => _radius;
        private set => _radius = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Center, Radius);
    }

    public T Detect()
    {
        foreach (var collider in FindColliders())
        {
            if (collider.TryGetComponent(out T target))
            {
                return target;
            }
        }

        return null;
    }
    
    public IEnumerable<T> DetectAll()
    {
        var targets = new List<T>();

        foreach (var collider in FindColliders())
        {
            if (collider.TryGetComponent(out T target))
            {
                targets.Add(target);
            }
        }

        return targets;
    }

    private IEnumerable<Collider2D> FindColliders()
    {
        return Physics2D.OverlapCircleAll(Center, Radius, _targetLayer);
    }
}
