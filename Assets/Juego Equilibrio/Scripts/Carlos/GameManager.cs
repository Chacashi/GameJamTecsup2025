using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] positionRamdom;
    [SerializeField] private Transform[] objetos;
    [SerializeField] private CanvasGroup panelLose;

    private void Start()
    {
        RandomPosition();
    }
    private void RandomPosition()
    {
        if (positionRamdom.Length < objetos.Length)
        {
            Debug.LogWarning("Hay más objetos que posiciones, algunos objetos compartirán posición.");
        }

        Transform[] posicionesMezcladas = new Transform[positionRamdom.Length];
        positionRamdom.CopyTo(posicionesMezcladas, 0);

        for (int i = 0; i < posicionesMezcladas.Length; i++)
        {
            int randomIndex = Random.Range(i, posicionesMezcladas.Length);
            Transform temp = posicionesMezcladas[i];
            posicionesMezcladas[i] = posicionesMezcladas[randomIndex];
            posicionesMezcladas[randomIndex] = temp;
        }

        for (int i = 0; i < objetos.Length; i++)
        {
            if (i < posicionesMezcladas.Length)
            {
                objetos[i].position = posicionesMezcladas[i].position;
            }
            else
            {
                objetos[i].position = posicionesMezcladas[i % posicionesMezcladas.Length].position;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < positionRamdom.Length; i++)
        {
            Gizmos.DrawSphere(positionRamdom[i].position, 0.1f);
        }
    }
    
}

