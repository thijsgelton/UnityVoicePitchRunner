using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void BackToMenu(float delayInSeconds)
    {
        Invoke("BackToMenu", delayInSeconds);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ResetLevel(float delayInSeconds)
    {
        Invoke("ResetLevel", delayInSeconds);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
