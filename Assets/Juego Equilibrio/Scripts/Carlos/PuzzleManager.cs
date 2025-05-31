using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public List<ArrastrarPieza> piezas;

    private int piezasColocadas = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PiezaColocadaCorrectamente(ArrastrarPieza pieza)
    {
        if (!pieza.estaColocada)
        {
            pieza.estaColocada = true;
            piezasColocadas++;

            if (piezasColocadas >= piezas.Count)
            {
                Debug.Log("¡Rompecabezas completado!");
            }
        }
    }
}
