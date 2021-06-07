using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class IdlingState : BaseState
    {
        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void Update()
        {
            if (PlayerIsGrounded() && PlayerMoveInput())
            {
                _player.TransitionToState(_player.movingState);
            }

            if (PlayerIsGrounded() && _player.JumpInput)
            {
                _player.TransitionToState(_player.jumpingState);
            }

            if (!PlayerIsGrounded())
            {
                _player.TransitionToState(_player.fallingState);
            }
        }

        private bool PlayerMoveInput()
        {
            return Mathf.Abs(_player.HorizontalInput) > 0f;
        }
    }
}

