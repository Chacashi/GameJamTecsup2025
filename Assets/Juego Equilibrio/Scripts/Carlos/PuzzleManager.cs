using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public List<ArrastrarPieza> piezas;

    private int piezasColocadas = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void PiezaColocadaCorrectamente(ArrastrarPieza pieza)
    {
        piezasColocadas++;
        if (piezasColocadas >= piezas.Count)
        {
            Debug.Log("¡Rompecabezas completado!");
        }
    }
}
