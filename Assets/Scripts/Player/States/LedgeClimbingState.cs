using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class LedgeClimbingState : BaseState
    {
        public override void EnterState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isLedgeClimbingHash, true);
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isLedgeClimbingHash, false);

            _player.ledgeClimbCompleted = false;
        }

        public override void Update()
        {
            if (_player.ledgeClimbCompleted)
            {
                ResetPlayerPos();

                _player.TransitionToState(_player.idlingState);
            }
        }

        private void ResetPlayerPos()
        {
            Vector3 endPos = new Vector3();
            
            switch (_player.ledgeGrabID)
            {
                case 0:
                    endPos.x = 0.75f;
                    endPos.y = 0.79f;
                    endPos.z = _player.transform.localPosition.z;

                    _player.gameObject.transform.localPosition = endPos;
                    break;

                case 1:
                    endPos.x = -1.3f;
                    endPos.y = 0.79f;
                    endPos.z = _player.transform.localPosition.z;

                    _player.gameObject.transform.localPosition = endPos;
                    break;

                default:
                    break;
            }
            _player.transform.SetParent(null);

            TogglePlayerController(true);

            _player.movement = Vector3.zero;

            SetPlayerControllerOnGround(_player.movement.x, _player.movement.y);
        }
    }
}

