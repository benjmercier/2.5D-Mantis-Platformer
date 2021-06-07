using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class JumpingState : BaseState
    {
        public override void EnterState()
        {
            // jump input was reading true over multiple frames and immediately triggered double jump
            _player.JumpInput = false; 

            _player.canDoubleJump = true;
            _player.canFallJump = false;

            _player.jumpVelocity = SetJumpVelocity();
        }

        public override void ExitState()
        {
            
        }

        public override void Update()
        {
            CalculateJump();

            if (_player.canDoubleJump && _player.JumpInput)
            {
                _player.TransitionToState(_player.doubleJumpingState);
            }

            if (_player.canWallJump && _player.JumpInput)
            {
                _player.TransitionToState(_player.wallJumpingState);
            }

            if (PlayerIsFalling(_player.jumpVelocity))
            {
                _player.TransitionToState(_player.fallingState);
            }
        }
    }
}

