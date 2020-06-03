using UnityEngine;
using UnityEngine.Audio;

namespace RPGM.Gameplay
{
    public class MusicController : MonoBehaviour
    {
        public AudioMixerGroup audioMixerGroup;
        public AudioClip audioClip;
        public float crossFadeTime = 3;

        AudioSource audioSourceA, audioSourceB;
        float audioSourceAVolumeVelocity, audioSourceBVolumeVelocity;

        private void Awake()
        {
            InvokeRepeating("IncreaseSpeed", 1f, 1f);
        }

        public void CrossFade(AudioClip audioClip)
        {
            var t = audioSourceA;
            audioSourceA = audioSourceB;
            audioSourceB = t;
            audioSourceA.clip = audioClip;
            audioSourceA.Play();
        }

        void IncreaseSpeed()
        {
            audioSourceA.pitch = audioSourceA.pitch * 1.005f;
        }

        void Update()
        {
            audioSourceA.volume = Mathf.SmoothDamp(audioSourceA.volume, 1f, ref audioSourceAVolumeVelocity, crossFadeTime, 1);
            audioSourceB.volume = Mathf.SmoothDamp(audioSourceB.volume, 0f, ref audioSourceBVolumeVelocity, crossFadeTime, 1);
        }

        void OnEnable()
        {
            audioSourceA = gameObject.AddComponent<AudioSource>();
            audioSourceA.spatialBlend = 0;
            audioSourceA.clip = audioClip;
            audioSourceA.loop = true;
            audioSourceA.outputAudioMixerGroup = audioMixerGroup;
            audioSourceA.Play();

            audioSourceB = gameObject.AddComponent<AudioSource>();
            audioSourceB.spatialBlend = 0;
            audioSourceB.loop = true;
            audioSourceB.outputAudioMixerGroup = audioMixerGroup;
        }
    }
}