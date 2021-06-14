using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class DoubleJumpingState : BaseState
    {
        public override void EnterState()
        {
            if (PlayerIsMoving(false))
            {
                //SetPlayerRotation();
            }

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

