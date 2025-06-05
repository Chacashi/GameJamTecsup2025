using UnityEngine;

public class EnableLingh : InteractiveObject
{
    [SerializeField] private Light[] luces;
    [SerializeField] private MeshRenderer[] meshRenderers;
    [SerializeField] private Material encendido;
    [SerializeField] private Material apagado;

    protected override void Interactive()
    {
        bool anyLightOn = false;

        for (int i = 0; i < luces.Length; i++)
        {
            luces[i].enabled = !luces[i].enabled;
            if (luces[i].enabled) anyLightOn = true;
        }

        Material nuevoMaterial = anyLightOn ? encendido : apagado;

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = nuevoMaterial;
        }
    }
}
