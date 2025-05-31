using UnityEngine;

public class SensivitySlider : AudioSlider
{
    [SerializeField] private Setting settingsData;
    private void OnEnable()
    {
        settingsData.OnUpdateVolume += UpdateText;
        settingsData.UpdateVolume(settingsData.Sensibility);
        slider.onValueChanged.AddListener(settingsData.UpdateVolume);
    }


    private void Start()
    {
        float savedValue = PlayerPrefs.GetFloat(settingsData.Key, 0.5f);
        settingsData.UpdateVolume(savedValue);
        slider.value = savedValue;
        volumeText.text = (savedValue * 100).ToString("000");
        
    }

    private void OnDisable()
    {
        settingsData.OnUpdateVolume -= UpdateText;
        slider.onValueChanged.RemoveListener(settingsData.UpdateVolume);
    }
}
