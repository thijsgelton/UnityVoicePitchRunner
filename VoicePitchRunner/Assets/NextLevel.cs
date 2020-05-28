using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int LevelNumber = -1;

    public void GoToNextLevel(int waitForSeconds) {
        StartCoroutine(GoToNextLevelMethod(waitForSeconds));
    }

    private IEnumerator GoToNextLevelMethod(int waitForSeconds) {
        yield return new WaitForSeconds (waitForSeconds);
        if(LevelNumber > -1) {
            SceneManager.LoadScene(LevelNumber);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
