using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class DoFade : MonoBehaviour
{
    private Image image;
    private TMP_Text text;
    private CanvasGroup canvasGroup;

    [SerializeField] private float timeFadeIN;
    [SerializeField] private float timeFadeOut;
    public float TimeFadeIN => timeFadeIN;
    public float TimeFadeOut => timeFadeOut;

    [Header("Option")]
    [SerializeField] private OptionFade option;

    private void Awake()
    {
        switch (option)
        {
            case OptionFade.Image:
                image = GetComponent<Image>();
                break;
            case OptionFade.TMP_Text:
                text = GetComponent<TMP_Text>();
                break;
            case OptionFade.CanvasGroup:
                canvasGroup = GetComponent<CanvasGroup>();
                break;
        }
    }
    public void FadeIN()
    {
        if (image != null)
        {
            ConfirmFadeIN(image);
        }
        else if (text != null)
        {
            ConfirmFadeIN(text);
        }
        else if (canvasGroup != null)
        {
            ConfirmFadeIN(canvasGroup);
        }
    }
    public void FadeOut()
    {
        if (image != null)
        {
            ConfirmFadeOut(image);
        }
        else if (text != null)
        {
            ConfirmFadeOut(text);
        }
        else if (canvasGroup != null)
        {
            ConfirmFadeOut(canvasGroup);
        }
    }
    //private void ConfirmFadeIN<T>(T value)
    //{
    //    dynamic newValue = value;
    //    newValue.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    //}
    private void ConfirmFadeIN(Image img)
    {
        img.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    }

    private void ConfirmFadeOut(Image img)
    {
        img.DOFade(0, timeFadeOut).SetEase(Ease.Linear);
    }

    private void ConfirmFadeIN(TMP_Text txt)
    {
        txt.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    }
    //private void ConfirmFadeOut<T>(T value)
    //{
    //    dynamic newValue = value;
    //    newValue.DOFade(0, timeFadeOut).SetEase(Ease.Linear);

    //}
    private void ConfirmFadeOut(TMP_Text txt)
    {
        txt.DOFade(0, timeFadeOut).SetEase(Ease.Linear);
    }
    private void ConfirmFadeIN(CanvasGroup canva)
    {
        canva.DOFade(1, timeFadeIN).SetEase(Ease.Linear);
    }

    private void ConfirmFadeOut(CanvasGroup canva)
    {
        canva.DOFade(0, timeFadeOut).SetEase(Ease.Linear);
    }
}
public enum OptionFade
{
    Image,
    TMP_Text,
    CanvasGroup
}