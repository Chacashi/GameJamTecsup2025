using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider barTime;
    [SerializeField] private Image statePlayer;


    private void Update()
    {
        barTime.value 
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
        barTime.value++; 
    }




    
    


}
