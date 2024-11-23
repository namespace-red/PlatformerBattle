using System;
using UnityEngine;

public class WorldSmoothHealthBar : SmoothHealthBar
{
    [SerializeField] private Canvas _canvas;
    
    protected override void Awake()
    {
        if (_canvas == null) 
            throw new NullReferenceException(nameof(_canvas));
        
        base.Awake();
        
        transform.SetParent(_canvas.transform);

        Health.Died += DeleteSelf;
    }

    private void DeleteSelf()
    {
        Destroy(gameObject, 1 / FillingSpeed);
    }
}
