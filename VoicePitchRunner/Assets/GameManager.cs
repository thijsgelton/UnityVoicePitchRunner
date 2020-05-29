using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float IdleThreshold;
    public string MicroPhone = Microphone.devices[0];
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void BackToMenu(float delayInSeconds)
    {
        Invoke("BackToMenu", delayInSeconds);
    }


    public void SetMicrophone(int option)
    {
        MicroPhone = Microphone.devices[option];
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
