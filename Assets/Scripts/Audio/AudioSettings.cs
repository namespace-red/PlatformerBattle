using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const string UiVolume = nameof(UiVolume);
    private const string MusicVolume = nameof(MusicVolume);
    private const float MinVolumeDb = -80;
    private const float MaxVolumeDb = 0;
    
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Toggle _toggleSound;
    [SerializeField] private Slider _sliderMasterVolume;
    [SerializeField] private Slider _sliderUiVolume;
    [SerializeField] private Slider _sliderMusicVolume;

    private void Awake()
    {
        if (_audioMixer == null) 
            throw new NullReferenceException(nameof(_audioMixer));
        
        if (_toggleSound == null) 
            throw new NullReferenceException(nameof(_toggleSound));
        
        if (_sliderMasterVolume == null) 
            throw new NullReferenceException(nameof(_sliderMasterVolume));
        
        if (_sliderUiVolume == null) 
            throw new NullReferenceException(nameof(_sliderUiVolume));
        
        if (_sliderMusicVolume == null) 
            throw new NullReferenceException(nameof(_sliderMusicVolume));
    }

    private void OnEnable()
    {
        _toggleSound.onValueChanged.AddListener(OnToggleSound);
        _sliderMasterVolume.onValueChanged.AddListener(OnMasterVolumeChanged);
        _sliderUiVolume.onValueChanged.AddListener(OnUiVolumeChanged);
        _sliderMusicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    private void OnDisable()
    {
        _toggleSound.onValueChanged.RemoveListener(OnToggleSound);
        _sliderMasterVolume.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        _sliderUiVolume.onValueChanged.RemoveListener(OnUiVolumeChanged);
        _sliderMusicVolume.onValueChanged.RemoveListener(OnMusicVolumeChanged);
    }

    private void OnToggleSound(bool flag)
    {
        SetVolume(MasterVolume, flag ? _sliderMasterVolume.value : 0);
    }
    
    private void OnMasterVolumeChanged(float value)
    {
        SetVolume(MasterVolume, value);
    }

    private void OnUiVolumeChanged(float value)
    {
        SetVolume(UiVolume, value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        SetVolume(MusicVolume, value);
    }

    private void SetVolume(string volume, float value)
    {
        value = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * (MaxVolumeDb - MinVolumeDb) / 4f + MaxVolumeDb;
        _audioMixer.SetFloat(volume, value);
    }
}
