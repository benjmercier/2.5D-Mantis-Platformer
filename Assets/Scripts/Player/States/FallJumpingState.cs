using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class FallJumpingState : BaseState
    {
        public override void EnterState()
        {
            _player.canFallJump = false;

            _player.jumpVelocity = SetJumpVelocity(_player.doubleJumpHeight);
        }

        public override void ExitState()
        {
            
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

