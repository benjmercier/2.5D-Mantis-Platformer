using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class WallJumpingState : BaseState
    {
        public override void EnterState()
        {
            _player.isVerticalStopped = false;

            _player.Animator.SetBool(_player.AnimParameters.isJumpingHash, true);

            SetWallJumpVelocity();
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isJumpingHash, false);
        }

        public override void Update()
        {
            CalculateJumpWithStop();

            if (PlayerIsMoving(true))
            {
                SetPlayerRotation();
            }

            if (_player.canWallJump && _player.JumpInput)
            {
                SetWallJumpVelocity();
            }

            if (_player.grabLedge)
            {
                _player.TransitionToState(_player.ledgeGrabbingState);
            }

            if (PlayerIsFalling(_player.jumpVelocity) || _player.isVerticalStopped)
            {
                _player.TransitionToState(_player.fallingState);
            }
        }

        private void SetWallJumpVelocity()
        {
            _player.movement = _player.wallJumpVelocity;

            _player.jumpVelocity = Mathf.Sqrt(_player.jumpHeight * -2 * _player.fallingGravity) / 1.1f;

            _player.canWallJump = false;
            _player.JumpInput = false;
        }
    }
}

