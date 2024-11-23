using System;
using System.Collections;
using UnityEngine;

public class SmoothBar : SimpleBar
{
    [SerializeField] private float _fillingSpeed = 1f;
    
    private Coroutine _coroutine;

    public float FillingSpeed
    {
        get => _fillingSpeed;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(FillingSpeed));
            
            _fillingSpeed = value;
        }
    }

    protected void SetValueSmoothly(float value)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _coroutine = StartCoroutine(ChangeSliderSmoothly(value));
    }

    private IEnumerator ChangeSliderSmoothly(float value)
    {
        while (Slider.value != value)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, value, _fillingSpeed * Time.deltaTime);
            yield return null;
        }

        _coroutine = null;
    }
}
