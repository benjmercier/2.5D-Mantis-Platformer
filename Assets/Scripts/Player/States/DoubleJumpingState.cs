using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class DoubleJumpingState : BaseState
    {
        public override void EnterState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isDoubleJumpingHash, true);

            _player.jumpVelocity = SetJumpVelocity(_player.doubleJumpHeight);
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isDoubleJumpingHash, false);

            _player.canDoubleJump = false;
        }

        public override void Update()
        {
            CalculateJump();

            if (PlayerIsFalling(_player.jumpVelocity))
            {
                _player.TransitionToState(_player.fallingState);
            }
        }
    }
}

