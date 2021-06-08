using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class FallJumpingState : BaseState
    {
        public override void EnterState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isJumpingHash, true);

            _player.canFallJump = false;

            _player.jumpVelocity = SetJumpVelocity(_player.doubleJumpHeight);
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isJumpingHash, false);
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

