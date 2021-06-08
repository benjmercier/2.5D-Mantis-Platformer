using UnityEngine;
using Mantis.Scripts.Player.Controller;

namespace Mantis.Scripts.Player.States
{
    public abstract class BaseState
    {
        protected PlayerControllerFSM _player;
        public PlayerControllerFSM Player { get { return _player; } }

        // Initially assigns _player to Player
        public virtual void AssignPlayer(PlayerControllerFSM playerController)
        {
            _player = playerController;
        }

        // Called when entering each state
        public abstract void EnterState();

        // Called when exiting each state
        public abstract void ExitState();

        // Called in player's Update()
        public abstract void Update();

        // Checks controller for collision 
        public virtual void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_player.Controller.isGrounded && hit.transform.CompareTag("Wall"))
            {
                float b = 1f;
                Vector3 V = _player.movement;
                Vector3 N = hit.normal;

                _player.wallJumpVelocity = b * (-2 * Vector3.Dot(V, N) * N + V);

                _player.canWallJump = true;
                _player.canDoubleJump = false;
            }
            else
            {
                _player.canWallJump = false;
            }
        }

        // Checks Ledge Grab for trigger
        public virtual void OnTriggerEnter(Collider collider)
        {
            // have player register to event instead
        }

        // Sets player look rotation
        protected virtual void SetPlayerRotation()
        {
            _player.lookRotation = _player.transform.localRotation;
            _player.lookRotation.SetLookRotation(_player.movement);

            _player.transform.localRotation = _player.lookRotation;

            _player.transform.rotation = Quaternion.Euler(0f, _player.transform.eulerAngles.y, 0f);
        }

        // Checks if controller is grounded
        protected virtual bool PlayerIsGrounded()
        {
            return _player.Controller.isGrounded;
        }

        // Checks for movement input
        protected virtual bool PlayerMoveInput()
        {
            return Mathf.Abs(_player.HorizontalInput) > 0f;
        }

        // Checks if the player's current pos equals previous pos
        protected virtual bool PlayerIsMoving(bool isWallJumping)
        {
            if (isWallJumping)
            {
                return _player.movement.x != 0f;
            }
            else
            {
                return _player.currentXPos != _player.previousXPos;
            }
        }

        // Returns the player's current x pos
        protected virtual float ReturnPlayerXPos()
        {
            return _player.transform.position.x;
        }

        // Moves the controller if not jumping
        protected virtual void PerformMovement(float x, float y, bool isGrounded)
        {
            _player.movement = new Vector3(x, ApplyDownwardForce(y, isGrounded), 0f);

            _player.Controller.Move(_player.movement * Time.deltaTime);
        }

        // Moves the controller if jumping
        protected virtual void PerformMovement(float x, float jumpVelocity)
        {
            _player.movement = new Vector3(x, jumpVelocity, 0f);

            _player.Controller.Move(_player.movement * Time.deltaTime);
        }

        // Applies downward force to keep the controller grounded or simulate gravity
        private float ApplyDownwardForce(float yMovement, bool isGrounded)
        {
            if (isGrounded)
            {
                return _player.groundedGravity;
            }
            else
            {
                _player.downwardForce = Mathf.Lerp(yMovement, _player.fallingGravity,
                _player.downwardAcceleration * Time.deltaTime);

                return _player.downwardForce;
            }
        }

        // Calls jump movement and calculates continuing jump velocity
        protected virtual void CalculateJump()
        {
            PerformMovement(_player.movement.x, _player.jumpVelocity);

            _player.jumpVelocity += _player.fallingGravity * Time.deltaTime;
        }

        // Sets initial jump velocity for single jump
        protected virtual float SetJumpVelocity()
        {
            return Mathf.Sqrt(_player.jumpHeight * -2 * _player.fallingGravity);
        }
        
        // Sets initial jump velocity for double and falling jump
        protected virtual float SetJumpVelocity(float jumpHeight)
        {
            jumpHeight = _player.jumpHeight * _player.doubleJumpMagnitude;

            return Mathf.Sqrt(jumpHeight * -2 * _player.fallingGravity);
        }

        // Checks if the player's y velocity is < 0 (falling)
        protected virtual bool PlayerIsFalling(float velocity)
        {
            return velocity < 0;
        }
    }
}

