using System;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(fileName = "Audio Settings SO", menuName = "ScriptableObjects/Game Systems/Audio/Mixer Data")]
public class AudioSettings : ScriptableObject
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string audioMixerKey;
    [SerializeField] protected string audioKeySafe;
    [SerializeField, Range(0, 1)] private float volumeScaled = 1;
    [SerializeField, Range(-80, 20)] private float volumeDBs = 0;
    [SerializeField] private bool isMuted;

    public float VolumeScaled => volumeScaled;
    public string Key => audioKeySafe;
    public Action<float> OnUpdateVolume;

    public void SaveDataFile()
    {
        PlayerPrefs.SetFloat(audioKeySafe, volumeScaled);
    }

    public void DeleteSafeData()
    {
        PlayerPrefs.DeleteKey(audioKeySafe);
    }

    public virtual void UpdateVolume(float value)
    {
        volumeScaled = value;

        volumeDBs = ToDecibels(volumeScaled);

        audioMixer.SetFloat(audioMixerKey, volumeDBs);

        OnUpdateVolume?.Invoke(volumeScaled);
    }

    private float ToDecibels(float value)
    {
        return Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 20);
    }
}