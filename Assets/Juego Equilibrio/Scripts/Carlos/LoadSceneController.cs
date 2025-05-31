using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    public void Cambiar(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Cerrar()
    {
        Application.Quit();
    }
}
