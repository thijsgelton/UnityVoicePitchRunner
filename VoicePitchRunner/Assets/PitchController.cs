using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Sends user input to the correct control systems.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PitchController : MonoBehaviour
{
    AudioSource _audio;
    public AudioMixerGroup microphone, master;

    public float PitchValue, AveragedPitchValue;

    public List<float> lastNSamples = new List<float>();

    private const int QSamples = 1024;
    private float[] _spectrum;
    private int _fSample;

    void Start()
    {
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;

        _audio = GetComponent<AudioSource>();
        _audio.outputAudioMixerGroup = microphone;
        Microphone.devices.ToList().ForEach(i => Debug.Log(i.ToString()));
        _audio.clip = Microphone.Start(Microphone.devices[0], true, 10, _fSample);
        _audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0))
        {

        }
        _audio.Play();
    }
    

    public float GetPitch()
    {
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        var maxValue = _spectrum.Max();
        var maxIndex = _spectrum.ToList().IndexOf(maxValue);
        float freqN = maxIndex;
        if (maxIndex > 0 && maxIndex < QSamples - 1)
        {
            var dL = _spectrum[maxIndex - 1] / _spectrum[maxIndex];
            var dR = _spectrum[maxIndex + 1] / _spectrum[maxIndex];
            freqN += 0.5f * (Mathf.Pow(dR, 2) - Mathf.Pow(dL, 2));
        }
        PitchValue = freqN * (_fSample / 3) / QSamples;
        AddPitchToList(PitchValue);
        AveragedPitchValue = lastNSamples.Sum() / 3;
        return AveragedPitchValue;
    }

    public void AddPitchToList(float pitch)
    {
        if(lastNSamples.Count < 3)
        {
            lastNSamples.Add(pitch);
        } else
        {
            lastNSamples.RemoveAt(0);
            lastNSamples.Add(pitch);
        }
    }
}
