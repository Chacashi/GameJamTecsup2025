using System;
using UnityEngine;

public abstract class SoundEmitter : MonoBehaviour
{
    [Header("Event Sound")]
    [SerializeField] private float distanceSound;
    public static event Action<Vector3, float> OnEventSound;
    protected void ActiveEventSound()
    {
        OnEventSound?.Invoke(transform.position, distanceSound);
    }
}
