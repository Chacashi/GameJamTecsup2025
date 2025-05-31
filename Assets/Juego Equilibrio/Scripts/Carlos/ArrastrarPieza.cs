using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastrarPieza : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 posicionInicial;

    private Vector2 posicionClick;
    private bool arrastrando = false;
    [SerializeField] private float umbralClick = 10f;

    public RectTransform posicionObjetivo;    
    public float rotacionObjetivo = 0f;

    [SerializeField] private float umbralPosicion = 20f; 
    [SerializeField] private float umbralRotacion = 5f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        posicionInicial = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        posicionClick = eventData.position;
        arrastrando = false;
    }
    public bool EstaEnPosicionCorrecta()
    {
        float distancia = Vector2.Distance(rectTransform.anchoredPosition, posicionObjetivo.anchoredPosition);

        float anguloActual = rectTransform.eulerAngles.z;
        float diferenciaRotacion = Mathf.Abs(Mathf.DeltaAngle(anguloActual, rotacionObjetivo));

        return distancia <= umbralPosicion && diferenciaRotacion <= umbralRotacion;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        arrastrando = true;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (EstaEnPosicionCorrecta())
        {
            rectTransform.anchoredPosition = posicionObjetivo.anchoredPosition;
            rectTransform.eulerAngles = new Vector3(0, 0, rotacionObjetivo);
            canvasGroup.blocksRaycasts = false;

            posicionInicial = rectTransform.anchoredPosition;

            PuzzleManager.Instance.PiezaColocadaCorrectamente(this);
        }
        else
        {
            rectTransform.anchoredPosition = posicionInicial;
        }
    }

    private void Update()
    {
        if (!arrastrando && Input.GetMouseButtonUp(0))
        {
            float distancia = Vector2.Distance(Input.mousePosition, posicionClick);
            if (distancia < umbralClick)
            {
                rectTransform.Rotate(0f, 0f, 90f);
            }
        }
    }
}
