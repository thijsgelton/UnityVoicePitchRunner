using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

namespace RPGM.UI
{
    /// <summary>
    /// Sends user input to the correct control systems.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class VoiceInputController : MonoBehaviour
    {
        public float stepSize = 0.1f;
        GameModel model = Schedule.GetModel<GameModel>();

        public AudioMixerGroup microphone, master;

        AudioSource _audio;
                
        public float PitchValue;

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
            while(!(Microphone.GetPosition(null) > 0))
            {

            }
            _audio.Play();
            Debug.Log(model.player.gameObject.name);
        }

        void GetPitch()
        {
            GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
            var maxValue = _spectrum.Max();
            var maxIndex = _spectrum.ToList().IndexOf(maxValue);
            float freqN = maxIndex;
            if (maxIndex > 0 && maxIndex < QSamples - 1)
            { 
                var dL = _spectrum[maxIndex - 1] / _spectrum[maxIndex];
                var dR = _spectrum[maxIndex + 1] / _spectrum[maxIndex];
                freqN += 0.5f * (Mathf.Pow(dR,2) - Mathf.Pow(dL, 2));
            }
            PitchValue = freqN * (_fSample / 2) / QSamples;
        }

        
        public enum State
        {
            CharacterControl,
            DialogControl,
            Pause
        }

        State state;

        public void ChangeState(State state) => this.state = state;

        void Update()
        {
            GetPitch();
            switch (state)
            {
                case State.CharacterControl:
                    CharacterControl();
                    break;
            }
        }


        void CharacterControl()
        {            
            if(PitchValue > 500)
            {
                Debug.Log("Ja");
                model.player.nextMoveCommand = Vector3.left * stepSize;
            } else if (PitchValue < 100)
            {
                model.player.nextMoveCommand = Vector3.zero;
            } else
            {
                model.player.nextMoveCommand = Vector3.right * stepSize;
            }
            Debug.Log(model.player.nextMoveCommand);


        }
    }
}