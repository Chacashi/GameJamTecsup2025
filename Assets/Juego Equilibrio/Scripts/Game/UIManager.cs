using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider barTime;
    [SerializeField] private Image statePlayer;
    [SerializeField] private float maxValuePlayer;



    private void Start()
    {
        barTime.minValue = 0;
       // barTime.maxValue = 
    }
    private void Update()
    {
        //barTime.value 
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
