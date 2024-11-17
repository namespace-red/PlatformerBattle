using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private Button _button;

    private void Awake()
    {
        if (_audioSource == null)
            throw new NullReferenceException(nameof(_audioSource));
        
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _audioSource.Play();
    }
}
