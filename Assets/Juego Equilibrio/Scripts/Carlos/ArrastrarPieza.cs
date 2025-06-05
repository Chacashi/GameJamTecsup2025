using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastrarPieza : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 posicionInicial;

    private Vector2 posicionClick;
    private bool arrastrando = false;
    [SerializeField] private float umbralClick = 10f;

    public RectTransform posicionObjetivo;
    public float rotacionObjetivo = 0f;

    [SerializeField] private float umbralPosicion = 20f;
    [SerializeField] private float umbralRotacion = 5f;

    [Header("Positions")]
    [SerializeField] private Vector3 position;
    public bool estaColocada = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        position = rectTransform.position; // Posición original de la pieza
        posicionInicial = position;        // La inicial también es la original al inicio
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        posicionClick = eventData.position;
        arrastrando = false;
    }

    public bool EstaEnPosicionCorrecta()
    {
        float distancia = Vector2.Distance(rectTransform.position, posicionObjetivo.position);
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
        rectTransform.position += (Vector3)(eventData.delta / canvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (EstaEnPosicionCorrecta())
        {
            rectTransform.position = posicionObjetivo.position;
            rectTransform.eulerAngles = new Vector3(0, 0, rotacionObjetivo);
            canvasGroup.blocksRaycasts = false;

            PuzzleManager.Instance.PiezaColocadaCorrectamente(this);
        }
        else
        {
            rectTransform.position = posicionInicial;
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

        if (Vector2.Distance(rectTransform.position, posicionObjetivo.position) < 40f)
        {
            posicionInicial = posicionObjetivo.position;
        }
        else
        {
            posicionInicial = position;
        }
    }
}
