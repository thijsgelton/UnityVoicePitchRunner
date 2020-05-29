using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public double IdleThreshold;
    public string MicroPhone;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MicroPhone = Microphone.devices[0];
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
