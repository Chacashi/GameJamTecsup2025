using DG.Tweening;
using UnityEngine;
using System.Collections;

public class DoMove : MonoBehaviour
{
    [Header("DoMove")]
    [SerializeField] private Vector3 target;
    [SerializeField] private Ease ease;
    [SerializeField] private float time;

    public void Go()
    {
        transform.DOMove(target, time)
            .SetEase(ease)
            .OnComplete(() =>
            {
                StartCoroutine(DelayedCameraSwitch());
            });
    }

    private IEnumerator DelayedCameraSwitch()
    {
        yield return new WaitForSeconds(2f); // Espera 1 segundo
        GameManager.instance.UpdateCamera();
        Debug.Log("Cambio de cámara después de 1 segundo.");
    }
}
