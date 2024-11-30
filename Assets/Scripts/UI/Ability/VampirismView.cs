using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VampirismView : AbilityWithCoolDownView
{
    private const float MinZRingPosition = 0f;
    private const float MaxZRingPosition = 2f;
    
    [SerializeField] private int _ringSteps;
    
    private LineRenderer _lineRenderer;
    private Vampirism _vampirism;
    
    protected override void Awake()
    {
        base.Awake();

        _vampirism = (Vampirism) Ability;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (_lineRenderer.enabled)
            DrawRing();
    }

    protected override void OnRan()
    {
        base.OnRan();
        
        _lineRenderer.enabled = true;
    }

    protected override void OnFinished()
    {
        base.OnFinished();
        
        _lineRenderer.enabled = false;
    }
    
    private void DrawRing()
    {
        _lineRenderer.positionCount = _ringSteps;
        
        for (int i = 0; i < _ringSteps; i++)
        {
            float currentPercent = (float) i / _ringSteps;
            float currentRadian = currentPercent * 2 * Mathf.PI;

            float x = Mathf.Cos(currentRadian) * _vampirism.Radius;
            float y = Mathf.Sin(currentRadian) * _vampirism.Radius;
            float z = Random.Range(MinZRingPosition, MaxZRingPosition);
            Vector3 position = _vampirism.Center + new Vector3(x, y, z);
            
            _lineRenderer.SetPosition(i, position);
        }
    }
}
