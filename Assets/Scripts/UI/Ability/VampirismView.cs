using System;
using UnityEngine;
using UnityEngine.UI;

public class VampirismView : AbilityWithCoolDownView
{
    [SerializeField] private Image _backImage;
    
    protected override void Awake()
    {
        base.Awake();
        
        if (_backImage == null)
            throw new NullReferenceException(nameof(_backImage));
        
        _backImage.enabled = false;
    }

    protected override void OnRan()
    {
        base.OnRan();
        
        _backImage.enabled = true;
    }

    protected override void OnFinished()
    {
        base.OnFinished();
        
        _backImage.enabled = false;
    }

}
