using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFiller : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _startValue;
    [SerializeField, Range(0, 1)] private float _finishValue;

    private Image _image;
    private Coroutine _coroutine;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void StartFilling(float sec)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _coroutine = StartCoroutine(ChangeFillAmount(sec));
    }

    public void ResetFill()
    {
        _image.fillAmount = _startValue;
    }
    
    private IEnumerator ChangeFillAmount(float sec)
    {
        _image.fillAmount = _startValue;
        
        while (_image.fillAmount != _finishValue)
        {
            _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, _finishValue, Time.deltaTime / sec);
            yield return null;
        }
        
        _coroutine = null;
    }
}
