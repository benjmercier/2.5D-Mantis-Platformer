using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class LedgeGrabbingState : BaseState
    {
        public override void EnterState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isLedgeHangingHash, true);

            TogglePlayerController(false);

            SetPlayerPosOnLedge();
            _player.grabLedge = false;
            _player.canClimbLedge = true;
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isLedgeHangingHash, false);

            _player.canClimbLedge = false;
        }

        public override void Update()
        {
            if (_player.canClimbLedge && _player.InteractionInput)
            {
                _player.TransitionToState(_player.ledgeClimbingState);
            }
        }

        private void SetPlayerPosOnLedge()
        {
            _player.gameObject.transform.SetParent(_player.ledgeParent);

            switch (_player.ledgeGrabID)
            {
                case 0:
                    _player.transform.localPosition = _player.frontLedgeHandPos;
                    break;

                case 1:
                    _player.transform.localPosition = _player.rearLedgeHandPos;
                    break;

                default:
                    break;
            }
        }
    }
}

