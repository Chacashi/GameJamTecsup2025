using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private AudioSettings audioSettingsData;
    [Header("Sprites")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;

    private void OnEnable()
    {
        audioSettingsData.OnUpdateVolume += UpdateText;

        audioSettingsData.UpdateVolume(audioSettingsData.VolumeScaled);

        slider.onValueChanged.AddListener(audioSettingsData.UpdateVolume);
    }
    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(audioSettingsData.Key, 0.5f);
        audioSettingsData.UpdateVolume(savedVolume);
        slider.value = savedVolume;
        volumeText.text = (savedVolume * 100).ToString("000");
    }

    private void OnDisable()
    {
        audioSettingsData.OnUpdateVolume -= UpdateText;

        slider.onValueChanged.RemoveListener(audioSettingsData.UpdateVolume);
    }

    private void UpdateText(float value)
    {
        volumeText.text = (value * 100).ToString("000");
        slider.value = value;
        if (value == 1)
        {
            image.sprite = sprites[2];
        }
        else if (value >= 0.5f)
        {
            image.sprite = sprites[1];
        }
        else
        {
            image.sprite = sprites[0];
        }
    }
}