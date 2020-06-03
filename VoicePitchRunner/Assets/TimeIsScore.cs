using UnityEngine;
using UnityEngine.UI;

public class TimeIsScore : MonoBehaviour
{
    public Text[] scores;

    private void Awake()
    {
        InvokeRepeating("UpdateScore", 1f, 1f);
    }

    private void UpdateScore()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            CancelInvoke();
        }
        else { 
            for(int i = 0; i < scores.Length; i++) { 
                scores[i].text = Mathf.Round(Time.timeSinceLevelLoad).ToString();
            }
        }
    }
}
