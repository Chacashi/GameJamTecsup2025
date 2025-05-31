using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class PosProcesingController : MonoBehaviour
{
    private Volume volume;
    private DepthOfField dof;

    [Header("Referencias")]
    public UIManager uiManager;

    [Header("Configuración")]
    public float focusMin = 0.3f;
    public float focusMax = 20f;

    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Start()
    {
        if (volume.profile.TryGet(out dof))
        {
            dof.active = true;
        }
    }

    private void Update()
    {
        float value = uiManager.BarTime.value; 
        float t = 0f;
        if (value >= 10f)
        {
            t = Mathf.InverseLerp(10f, 20f, value);
            t = Mathf.Pow(t, 2f); 
        }
        dof.focusDistance.value = Mathf.Lerp(focusMax, focusMin, t);
    }
}
