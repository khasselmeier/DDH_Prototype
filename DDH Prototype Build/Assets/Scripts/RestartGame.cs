using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartGameButton()
    {
        SceneManager.LoadScene("Menu");
    }
}