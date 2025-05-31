using UnityEngine;
using DG.Tweening;
public class DoUI : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Ease ease;
    private RectTransform rectTransform;
    private Vector2 positionInitial;
    [SerializeField] private Vector2 target;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        positionInitial = rectTransform.anchoredPosition;
    }
    public void Open()
    {
        rectTransform.DOAnchorPos(target, time).SetEase(ease);
    }
    public void Close()
    {
        rectTransform.DOAnchorPos(positionInitial, time).SetEase(ease);
    }
}