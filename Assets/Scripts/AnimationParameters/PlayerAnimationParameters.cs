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
            isFalling = "isFalling";

        [HideInInspector]
        public int isIdlingHash,
            isMovingHash,
            isJumpingHash,
            isDoubleJumpingHash,
            isFallingHash;

        public PlayerAnimationParameters()
        {
            this.isIdlingHash = Animator.StringToHash(this.isIdling);
            this.isMovingHash = Animator.StringToHash(this.isMoving);
            this.isJumpingHash = Animator.StringToHash(this.isJumping);
            this.isDoubleJumpingHash = Animator.StringToHash(this.isDoubleJumping);
            this.isFallingHash = Animator.StringToHash(this.isFalling);
        }
    }
}

