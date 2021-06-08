using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class FallingState : BaseState
    {
        private bool _isStopped = false;

        public override void EnterState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isFallingHash, true);
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isFallingHash, false);
        }

        public override void Update()
        {
            CalculateFalling();

            if (PlayerIsGrounded() && !PlayerMoveInput())
            {
                _player.TransitionToState(_player.idlingState);
            }

            if (PlayerIsGrounded() && PlayerMoveInput())
            {
                _player.TransitionToState(_player.movingState);
            }

            if (_player.canDoubleJump && _player.JumpInput)
            {
                _player.TransitionToState(_player.doubleJumpingState);
            }

            if (_player.canFallJump && _player.JumpInput)
            {
                _player.TransitionToState(_player.fallJumpingState);
            }

            if (_player.canWallJump && _player.JumpInput)
            {
                _player.TransitionToState(_player.wallJumpingState);
            }

            if (_player.grabLedge)
            {
                _player.TransitionToState(_player.ledgeGrabbingState);
            }
        }

        private void CalculateFalling()
        {
            _player.previousXPos = ReturnPlayerXPos();

            PerformMovement(SetXMovementPos(), _player.movement.y, false);

            _player.currentXPos = ReturnPlayerXPos();

            _isStopped = !PlayerIsMoving(false);
        }

        private float SetXMovementPos()
        {
            if (!_isStopped)
            {
                _player.lerpXPos = _player.movement.x;
            }
            else
            {
                _player.lerpXPos = LerpXPos();
            }

            return _player.lerpXPos;
        }

        private float LerpXPos()
        {
            return Mathf.Lerp(_player.movement.x, 0f, _player.downwardAcceleration * Time.deltaTime);
        }
    }
}

