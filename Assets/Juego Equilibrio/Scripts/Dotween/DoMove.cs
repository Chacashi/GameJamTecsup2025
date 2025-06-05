using DG.Tweening;
using UnityEngine;

public class DoMove : MonoBehaviour
{
    [Header("DoMove")]
    [SerializeField] private Vector3 target;
    [SerializeField] private Ease ease;
    [SerializeField] private float time;
    public void Go()
    {
        transform.DOMove(target, time).SetEase(ease);
    }
}
