using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class IdlingState : BaseState
    {
        public override void EnterState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isIdlingHash, true);
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isIdlingHash, false);
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
    }
}

