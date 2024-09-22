using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartGameButton()
    {
        Debug.Log("Restarting the game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}