using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mantis.Scripts.Checkers
{
    public class GroundChecker : MonoBehaviour
    {
        private Vector3 _controllerBase;

        private bool _moveToGround = false;
        private RaycastHit _raycastHit;
        [SerializeField]
        private float _groundCheckDistance = 0.2f;

        public void CheckIfGrounded(CharacterController controller, float halfHeight, bool canDoubleJump)
        {
            if (controller.isGrounded)
            {
                return;
            }

            _controllerBase = controller.transform.localPosition - Vector3.up * halfHeight;
            _controllerBase.y += controller.center.y;

            _moveToGround = Physics.Raycast(_controllerBase, Vector3.down, out _raycastHit, _groundCheckDistance);

            if (_moveToGround && !canDoubleJump)
            {
                controller.Move(Vector3.down * _raycastHit.distance);
            }
        }
    }
}

