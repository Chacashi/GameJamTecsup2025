using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform[] positionRamdom;
    [SerializeField] private GameObject[] objetos;
    [SerializeField] private CanvasGroup panelLose;

    [Header("RompeCabezas")]
    [SerializeField] private int piezas;
    [SerializeField] private Transform[] positionRompecabeza;
    [SerializeField] private GameManager prefabPiezaRompecabeza;

    [SerializeField] private UIManager ui;

    [SerializeField] private CanvasGroup canvas1;
    [SerializeField] private CanvasGroup canvas2;

    //Events
    public static event Action OnPiecesCmplete;

    [Header("DoFade")]
    [SerializeField] private DoFade fade;

    [Header("Cameras")]
    [SerializeField] private GameObject CameraMain;
    [SerializeField] private GameObject CameraPortal;

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
        Time.timeScale = 1;
        RandomPosition();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            UpdateCamera();
        }
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
    public void UpdateCamera()
    {
        StartCoroutine(SwitchCameraWithFade());
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
            Instantiate(objetos[i], posicionesMezcladas[i].position,Quaternion.Euler(-90,0,0));
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
    public void Fail()
    {
        ui.FadePanelIn(canvas1);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void Win()
    {
        OnPiecesCmplete?.Invoke();
        //ui.FadePanelIn(canvas2);
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        //Time.timeScale = 0;
    }
    public void CheckRompeCabezas()
    {
        ui.IncrementPieceCount();
    }
    private IEnumerator SwitchCameraWithFade()
    {
        fade.FadeIN();

        yield return new WaitForSeconds(fade.TimeFadeIN);

        if (CameraMain.activeSelf)
        {
            CameraMain.SetActive(false);
            CameraPortal.SetActive(true);
        }
        else
        {
            CameraMain.SetActive(true);
            CameraPortal.SetActive(false);
        }

        fade.FadeOut();
    }
}

