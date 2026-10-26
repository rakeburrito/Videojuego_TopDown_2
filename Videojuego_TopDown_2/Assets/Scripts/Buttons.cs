using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
