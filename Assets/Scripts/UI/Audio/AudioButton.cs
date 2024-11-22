using System;
using UnityEngine;

public class AudioButton : ButtonHandler
{
    [SerializeField] private AudioSource _audioSource;
    
    protected override void Awake()
    {
        if (_audioSource == null)
            throw new NullReferenceException(nameof(_audioSource));
        
        base.Awake();
    }

    protected override void OnButtonClicked()
    {
        _audioSource.Play();
    }
}
