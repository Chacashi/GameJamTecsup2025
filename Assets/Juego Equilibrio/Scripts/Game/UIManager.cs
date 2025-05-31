using System;
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

    private AudioSource soundSilvido;
    public Slider BarTime => barTime;
    private void Awake()
    {
        soundSilvido = GetComponent<AudioSource>();
    }
    private void Start()
    {
        barTime.minValue = 0;
        barTime.maxValue = maxValuePlayer;
        barTime.value = maxValuePlayer/2;
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
        barTime.value +=valueNoise;
        soundSilvido.Play();
    }

    private void CalculateStatePlayer( float value)
    {
        if (value <= 0)
        {
            GameManager.instance.Fail();
        }
        else if(value<5 && value > 0)
        {
            statePlayer.sprite = arrayStatesPlayer[0];
        }
        else if(value>=5 && value < 12)
        {
            statePlayer.sprite = arrayStatesPlayer[1];
        }
        else if(value>=12 && value < 20)
        {
            statePlayer.sprite = arrayStatesPlayer[2];
        }
        else
        {
            GameManager.instance.Fail();
        }

    }

    void SubstractValue()
    {
        if (barTime.value > barTime.minValue)
        {
            barTime.value -= Time.deltaTime;
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










}
