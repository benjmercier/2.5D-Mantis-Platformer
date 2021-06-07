using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class WallJumpingState : BaseState
    {
        public override void EnterState()
        {
            SetWallJumpVelocity();
        }

        public override void ExitState()
        {
            
        }

        public override void Update()
        {
            CalculateJump();

            if (PlayerIsMoving(true))
            {
                SetPlayerRotation();
            }

            if (_player.canWallJump && _player.JumpInput)
            {
                SetWallJumpVelocity();
            }

            if (PlayerIsFalling(_player.jumpVelocity))
            {
                _player.TransitionToState(_player.fallingState);
            }
        }

        private void SetWallJumpVelocity()
        {
            _player.movement = _player.wallJumpVelocity;

            _player.jumpVelocity = Mathf.Sqrt(_player.jumpHeight * -2 * _player.fallingGravity) / 1.1f;

            _player.canWallJump = false;
        }
    }
}

