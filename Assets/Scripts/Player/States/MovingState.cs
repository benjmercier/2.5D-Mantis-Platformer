using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class MovingState : BaseState
    {
        public override void EnterState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isMovingHash, true);

            _player.canDoubleJump = false;

            SetVelocityToMovement();
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isMovingHash, false);

            _player.canFallJump = true;
        }

        public override void Update()
        {
            CalculateMovement();

            if (PlayerIsMoving(false))
            {
                SetPlayerRotation();
            }            

            if (!PlayerIsMoving(false))
            {
                _player.TransitionToState(_player.idlingState);
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

        private void SetVelocityToMovement()
        {
            _player.currentVelocity =  _player.movement;
        }

        private void CalculateMovement()
        {
            _player.previousXPos = ReturnPlayerXPos();

            CalculateMovementVelocity();
            PerformMovement(_player.currentVelocity.x, _player.currentVelocity.y, true);

            _player.currentXPos = ReturnPlayerXPos();
        }

        private void CalculateMovementVelocity()
        {
            _player.targetDirection = new Vector3(_player.transform.position.x + _player.HorizontalInput,
                _player.transform.position.y, 0f);

            _player.desiredVelocity = (_player.targetDirection - _player.transform.position).normalized;
            _player.desiredVelocity *= _player.MaxSpeed;

            _player.steeringVelocity = _player.desiredVelocity - _player.currentVelocity;
            _player.steeringVelocity = Vector3.ClampMagnitude(_player.steeringVelocity, _player.MaxForce);

            _player.currentVelocity += _player.steeringVelocity;
            _player.currentVelocity = Vector3.ClampMagnitude(_player.currentVelocity, _player.MaxSpeed);
        }
    }
}

