using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Setting", order = 1)]
public class Setting : AudioSettings
{
    [Range(0f, 1f)][SerializeField] private float sensibility;

    public float Sensibility => sensibility;
    public override void UpdateVolume(float value)
    {
        sensibility = value;
        OnUpdateVolume?.Invoke(sensibility);
    }   
}
