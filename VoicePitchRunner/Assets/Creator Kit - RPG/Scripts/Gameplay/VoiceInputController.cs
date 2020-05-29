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
        public GameManager manager;
        GameModel model = Schedule.GetModel<GameModel>();
        public PitchController pitchController;


        private void Awake()
        {
            manager = FindObjectOfType<GameManager>();
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
            switch (state)
            {
                case State.CharacterControl:
                    CharacterControl();
                    break;
            }
        }


        void CharacterControl()
        {
            var pitchValue = pitchController.GetPitch();
            if(pitchValue > 500)
            {
                model.player.nextMoveCommand = Vector3.left * stepSize;
            } else if (pitchValue < manager.IdleThreshold)
            {
                model.player.nextMoveCommand = Vector3.zero;
            } else
            {
                model.player.nextMoveCommand = Vector3.right * stepSize;
            }
        }
    }
}