using UnityEngine;
using System;

public class OidoEnemy : MonoBehaviour
{
    private Vector3 position;
    public event Action<Vector3> OnCollisionEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            OnCollisionEnter?.Invoke(other.transform.position);
        }
    }
}
