using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerMaster;
    [SerializeField] private Slider _sliderMasterVolume;
    
    private Toggle _toggleAudio;

    private void Awake()
    {
        if (_audioMixerMaster == null)
            throw new NullReferenceException(nameof(_audioMixerMaster));
        
        if (_sliderMasterVolume == null) 
            throw new NullReferenceException(nameof(_sliderMasterVolume));

        _toggleAudio = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggleAudio.onValueChanged.AddListener(OnToggleAudio);
    }

    private void OnDisable()
    {
        _toggleAudio.onValueChanged.RemoveListener(OnToggleAudio);
    }
    
    private void OnToggleAudio(bool flag)
    {
        float value = flag ? _sliderMasterVolume.value : 0;
        _audioMixerMaster.audioMixer.SetFloat(_audioMixerMaster.name, AudioSettings.ValueToVolume(value));
    }
}
