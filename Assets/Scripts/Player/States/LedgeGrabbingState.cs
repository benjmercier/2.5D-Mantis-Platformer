using UnityEngine;
using Mantis.Scripts.Managers;

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

            UIManager.Instance.ActivateNotification(true, UIManager.Instance.LedgeGrabTxt);
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isLedgeHangingHash, false);

            _player.canClimbLedge = false;

            UIManager.Instance.ActivateNotification(false, UIManager.Instance.LedgeGrabTxt);
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

