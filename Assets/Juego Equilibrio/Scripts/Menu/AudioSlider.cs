using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected TMP_Text volumeText;
    [SerializeField] private AudioSettings audioSettingsData;
    [Header("Sprites")]
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected Image image;

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

    public void UpdateText(float value)
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