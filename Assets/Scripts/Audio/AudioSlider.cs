using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    
    private Slider _slider;

    private void Awake()
    {
        if (_audioMixerGroup == null)
            throw new NullReferenceException(nameof(_audioMixerGroup));

        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnVolumeChanged);
    }
    
    private void OnVolumeChanged(float value)
    {
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, AudioSettings.ValueToVolume(value));
    }
}
