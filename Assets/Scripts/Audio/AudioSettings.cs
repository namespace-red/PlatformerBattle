using UnityEngine;

public static class AudioSettings
{
    public static readonly float MinVolumeDb = -80;
    public static readonly float MaxVolumeDb = 0;
    
    public static float ValueToVolume(float value)
    {
        return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * (MaxVolumeDb - MinVolumeDb) / 4f + MaxVolumeDb;
    }
}
