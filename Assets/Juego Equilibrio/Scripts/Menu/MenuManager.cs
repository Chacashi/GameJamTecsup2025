using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class MenuManager : MasterManager
{
    [SerializeField] private CanvasGroup introCanvas;
    [SerializeField] private CinemachineCamera camIntro;
    [SerializeField] private float durationFadeCanvas;
    [SerializeField] private CanvasGroup menuCanvas;
    [SerializeField] private float delayTransitionCameras;




    private void Start()
    {
        input.enabled = false;
        introCanvas.DOFade(1f, durationFadeCanvas).OnComplete(() =>
        {
            introCanvas.interactable = true;
            introCanvas.blocksRaycasts = true;
            input.enabled = true;
        });
    }
    public void TransitionToMenu()
    {
        input.enabled = false;
        FadeObject(introCanvas, 0f, durationFadeCanvas, () =>
        {
            camIntro.gameObject.SetActive(false);
            introCanvas.interactable = false;
            introCanvas.blocksRaycasts = false;
            StartCoroutine(TransitionCamera(menuCanvas));
        });
    }


    public void TransitionToIntro()
    {
        FadeObject(menuCanvas, 0f, durationFadeCanvas, () =>
        {
            camIntro.gameObject.SetActive(true);
            menuCanvas.interactable = false;
            menuCanvas.blocksRaycasts = false;
            StartCoroutine(TransitionCamera(introCanvas));

        });
    }




    IEnumerator TransitionCamera(CanvasGroup canvas)
    {
        yield return new WaitForSeconds(delayTransitionCameras);
        FadeObject(canvas, 1f, durationFadeCanvas, () =>
        {

            canvas.interactable = true;
            canvas.blocksRaycasts = true;
            if (canvas == introCanvas)
            {
                input.enabled = true;
            }

        });
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
