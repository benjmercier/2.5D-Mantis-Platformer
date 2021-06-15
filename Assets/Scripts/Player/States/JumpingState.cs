using UnityEngine;
using Mantis.Scripts.Managers;

namespace Mantis.Scripts.Player.States
{
    public class JumpingState : BaseState
    {
        public override void EnterState()
        {
            _player.isVerticalStopped = false;

            _player.Animator.SetBool(_player.AnimParameters.isJumpingHash, true);

            // jump input was reading true over multiple frames and immediately triggered double jump
            _player.JumpInput = false; 

            _player.canDoubleJump = true;
            _player.canFallJump = false;

            _player.jumpVelocity = SetJumpVelocity();
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isJumpingHash, false);

            UIManager.Instance.ActivateNotification(_player.canWallJump, UIManager.Instance.WallJumpTxt);
        }

        public override void Update()
        {
            CalculateJumpWithStop();

            if (_player.canDoubleJump && _player.JumpInput)
            {
                _player.TransitionToState(_player.doubleJumpingState);
            }
            
            UIManager.Instance.ActivateNotification(_player.canWallJump, UIManager.Instance.WallJumpTxt);

            if (_player.canWallJump && _player.JumpInput)
            {
                _player.TransitionToState(_player.wallJumpingState);
            }

            if (PlayerIsFalling(_player.jumpVelocity) || _player.isVerticalStopped)
            {
                _player.TransitionToState(_player.fallingState);
            }

            if (_player.grabLedge)
            {
                _player.TransitionToState(_player.ledgeGrabbingState);
            }

            if (_player.isAttachedToRope)
            {
                _player.TransitionToState(_player.ropeSwingingState);
            }
        }
    }
}

