using DG.Tweening;
using UnityEngine;

public class DoMove : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Ease ease;
    [SerializeField] private Vector3 target;
    private Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
    }

    public void Open()
    {
        transform.DOMove(target, time).SetEase(ease);
    }

    public void Close()
    {
        transform.DOMove(initialPosition, time).SetEase(ease);
    }
}
