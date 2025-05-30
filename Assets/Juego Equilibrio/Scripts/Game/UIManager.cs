using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider barTime;
    [SerializeField] private Image statePlayer;
    [SerializeField] private float maxValuePlayer;
    [SerializeField] private float valueNoise;
    [SerializeField] private Sprite[] arrayStatesPlayer;
    public static event Action OnPlayerDeathd;


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
    }

    private void OnDisable()
    {
        InputReader.OnChangeBarTime -= IncrementBarTime;
    }
    void IncrementBarTime()
    {
        barTime.value +=valueNoise; 
    }

    private void CalculateStatePlayer( float value)
    {
        if (value <= 0)
        {
            OnPlayerDeathd?.Invoke();
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
            OnPlayerDeathd?.Invoke();
        }

    }

    void SubstractValue()
    {
        if (barTime.value > barTime.minValue)
        {
            barTime.value -= Time.deltaTime;
        }
    }




    
    


}
