using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    public GameManager manager;
    public TMPro.TMP_Text pitchValue;
    public PitchController pitchController;
    private void Awake()
    {
        string[] options = Microphone.devices;
        dropdown.AddOptions(options.ToList());
    }

    public void Record()
    {
        float idlePitchValue = 0;
        int count = 0;
        for(int i = 0; i < 100; i ++)
        {
            idlePitchValue += pitchController.GetPitch();
            count++;
            pitchValue.text = Mathf.Round(idlePitchValue / count).ToString();
        }
        manager.IdleThreshold = Mathf.Round(idlePitchValue /count);
    }
}
