using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthText : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    private TMP_Text _text;

    private void Awake()
    {
        if (_health == null) 
            throw new NullReferenceException(nameof(Health));
        
        _text = GetComponent<TMP_Text>();
    }
    
    private void OnEnable()
    {
        _health.ValueChanged += SetText;
        SetText(_health.Value, _health.MaxValue);
    }

    private void OnDisable()
    {
        _health.ValueChanged -= SetText;
    }

    private void SetText(float value, float maxValue)
    {
        _text.text = $"{value}/{maxValue}";
    }
}
