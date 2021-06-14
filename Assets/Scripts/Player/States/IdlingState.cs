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
            // Lerp player x pos to stationary
            if (Mathf.Abs(_player.movement.x - 0f) > 0.1f)
            {
                _player.movement.x = LerpXPos();
            }

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

        private float LerpXPos()
        {
            return Mathf.Lerp(_player.movement.x, 0f, _player.downwardAcceleration * Time.deltaTime);
        }
    }
}

