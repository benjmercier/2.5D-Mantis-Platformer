using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class RopeSwingingState : BaseState
    {
        private bool _canDetach = false;

        public override void EnterState()
        {
            SetPlayerRotation();

            _player.Animator.SetBool(_player.AnimParameters.isRopeSwingingHash, true);

            TogglePlayerController(false);

            _player.transform.parent = _player.attachedRopeSegment.transform;

            _canDetach = true;
        }

        public override void ExitState()
        {
            _player.Animator.SetBool(_player.AnimParameters.isRopeSwingingHash, false);

            TogglePlayerController(true);

            _player.transform.parent = null;

            _player.transform.rotation = _player.playerRotation;

            SetPlayerRotation();
        }

        public override void Update()
        {
            CalculateRopeSwing();
            //SetPlayerPosAndRot();
            
            if (_player.InteractionInput && _canDetach)
            {
                _canDetach = false;

                _player.OnDetachFromRope();

                _player.TransitionToState(_player.doubleJumpingState);
            }
        }

        private void CalculateRopeSwing()
        {
            var vector = new Vector3(0, 0f, _player.HorizontalInput);
            
            _player.ropeGrabRB.AddRelativeForce(vector * _player.swingForce);
        }

        private void SetPlayerPosAndRot()
        {
            _player.transform.position = _player.attachedRopeSegment.transform.position;
            _player.transform.rotation = Quaternion.Euler(-_player.attachedRopeSegment.transform.localEulerAngles.z,
                _player.transform.localEulerAngles.y, 0f);
        }
    }
}

