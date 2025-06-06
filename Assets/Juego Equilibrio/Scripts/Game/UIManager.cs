using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MasterManager
{
    [SerializeField] private Slider barTime;
    [SerializeField] private Image statePlayer;
    [SerializeField] private float maxValuePlayer;
    [SerializeField] private float valueNoise;
    [SerializeField] private Sprite[] arrayStatesPlayer;
    [SerializeField] private CanvasGroup panelLose;
    [SerializeField] private CanvasGroup panelPause;
    [SerializeField] private TMP_Text textCountPieces;
    [SerializeField] private float numPieces = 0;

    public static event Action OnPiecePuzzleComplete;


    private AudioSource soundSilvido;
    public Slider BarTime => barTime;

    [Header("Silbido Config")]
    [SerializeField] private float silbidoFadeDelay = 3f;

    private Coroutine stopSilbidoCoroutine;

    private void Awake()
    {
        soundSilvido = GetComponent<AudioSource>();
    }

    private void Start()
    {
        textCountPieces.text = "00/06";
        barTime.minValue = 0;
        barTime.maxValue = maxValuePlayer;
        barTime.value = maxValuePlayer / 2;
    }

    private void Update()
    {
        SubstractValue();
        CalculateStatePlayer(barTime.value);
    }

    private void OnEnable()
    {
        InputReader.OnChangeBarTime += IncrementBarTime;
        InputReader.OnstopGame += DoPause;
    }

    private void OnDisable()
    {
        InputReader.OnChangeBarTime -= IncrementBarTime;
        InputReader.OnstopGame -= DoPause;
    }

    void IncrementBarTime()
    {
        barTime.value += valueNoise;

        if (!soundSilvido.isPlaying)
        {
            soundSilvido.Play();
        }

        if (stopSilbidoCoroutine != null)
            return;

        stopSilbidoCoroutine = StartCoroutine(StopSilbidoAfterDelay());
    }

    void SubstractValue()
    {
        if (barTime.value > barTime.minValue)
        {
            barTime.value -= Time.deltaTime;

            if (soundSilvido.isPlaying && stopSilbidoCoroutine == null)
            {
                stopSilbidoCoroutine = StartCoroutine(StopSilbidoAfterDelay());
            }
        }
    }

    private IEnumerator StopSilbidoAfterDelay()
    {
        yield return new WaitForSeconds(silbidoFadeDelay);
        soundSilvido.Stop();
        stopSilbidoCoroutine = null;
    }

    private void CalculateStatePlayer(float value)
    {
        if (value <= 0)
        {
            GameManager.instance.Fail();
        }
        else if (value < 5 && value > 0)
        {
            statePlayer.sprite = arrayStatesPlayer[0];
        }
        else if (value >= 5 && value < 12)
        {
            statePlayer.sprite = arrayStatesPlayer[1];
        }
        else if (value >= 12 && value < 19.5)
        {
            statePlayer.sprite = arrayStatesPlayer[2];
        }
        else
        {
            GameManager.instance.Fail();
        }
    }

    public void PauseGame(CanvasGroup pausePanel)
    {
        if (pausePanel.alpha == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            input.SwitchCurrentActionMap("Pause");
            input.enabled = false;
            Time.timeScale = 0;
            FadeObject(pausePanel, 1f, durationFadePanel, () =>
            {
                pausePanel.interactable = true;
                pausePanel.blocksRaycasts = true;
                input.enabled = true;
            });
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            input.SwitchCurrentActionMap("Game");
            input.enabled = false;
            FadeObject(pausePanel, 0f, durationFadePanel, () =>
            {
                pausePanel.interactable = false;
                pausePanel.blocksRaycasts = false;
                input.enabled = true;
                Time.timeScale = 1;
            });
        }
    }
    private void DoPause()
    {
        PauseGame(panelPause);
    }
    public void IncrementPieceCount()
    {
        numPieces++;
        textCountPieces.text = numPieces.ToString("00") + "/06";
        if (numPieces >= 6)
        {
            textCountPieces.text = numPieces.ToString("00") + "/06";
            OnPiecePuzzleComplete?.Invoke();
            GameManager.instance.Complete();
        }
    }



}
