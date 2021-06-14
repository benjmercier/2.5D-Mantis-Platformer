using UnityEngine;

namespace Mantis.Scripts.AnimationParameters
{
    [System.Serializable]
    public class PlayerAnimationParameters
    {
        [SerializeField]
        private string isIdling = "isIdling",
            isMoving = "isMoving",
            isJumping = "isJumping",
            isDoubleJumping = "isDoubleJumping",
            isFalling = "isFalling",
            isLedgeHanging = "isLedgeHanging",
            isLedgeClimbing = "isLedgeClimbing",
            isRopeSwinging = "isRopeSwinging";

        [HideInInspector]
        public int isIdlingHash,
            isMovingHash,
            isJumpingHash,
            isDoubleJumpingHash,
            isFallingHash,
            isLedgeHangingHash,
            isLedgeClimbingHash,
            isRopeSwingingHash;

        public PlayerAnimationParameters()
        {
            this.isIdlingHash = Animator.StringToHash(this.isIdling);
            this.isMovingHash = Animator.StringToHash(this.isMoving);
            this.isJumpingHash = Animator.StringToHash(this.isJumping);
            this.isDoubleJumpingHash = Animator.StringToHash(this.isDoubleJumping);
            this.isFallingHash = Animator.StringToHash(this.isFalling);
            this.isLedgeHangingHash = Animator.StringToHash(this.isLedgeHanging);
            this.isLedgeClimbingHash = Animator.StringToHash(this.isLedgeClimbing);
            this.isRopeSwingingHash = Animator.StringToHash(this.isRopeSwinging);
        }
    }
}

