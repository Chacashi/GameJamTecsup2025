using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform[] positionRamdom;
    [SerializeField] private Transform[] objetos;
    [SerializeField] private CanvasGroup panelLose;

    [Header("RompeCabezas")]
    [SerializeField] private int piezas;
    [SerializeField] private Transform[] positionRompecabeza;
    [SerializeField] private GameManager prefabPiezaRompecabeza;

    //Events
    public static event Action<GameManager> OnDestroy;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    private void Start()
    {
        RandomPosition();
    }
    private void DrawRompecabezas()
    {
        for (int i = 0; i < piezas; i++)
        {
            int randomIndex =  Random.Range(0, positionRompecabeza.Length);
            Transform spawnPoint = positionRompecabeza[randomIndex];
            Instantiate(prefabPiezaRompecabeza, spawnPoint.position, Quaternion.identity);
        }
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
        if(positionRamdom!=null) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < positionRamdom.Length; i++)
        {
            Gizmos.DrawSphere(positionRamdom[i].position, 0.1f);
        }
    }
    public void Fail()
    {
        
    }
    public void CheckRompeCabezas()
    {

    }
}

