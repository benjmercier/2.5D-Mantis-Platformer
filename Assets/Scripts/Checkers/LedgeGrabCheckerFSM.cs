using System;
using UnityEngine;
using Mantis.Scripts.Player.Controller;

namespace Mantis.Scripts.Checkers
{
    public class LedgeGrabCheckerFSM : MonoBehaviour
    {
        private Vector3 _defaultLocalPos;

        public static event Action<bool, int, Transform> onLedgeCollision;

        private void Start()
        {
            _defaultLocalPos = transform.localPosition;
        }

        private void OnEnable()
        {
            PlayerControllerFSM.onDetachFromRope += ResetPosition;
        }

        private void OnDisable()
        {
            PlayerControllerFSM.onDetachFromRope -= ResetPosition;
        }

        private void ResetPosition()
        {
            transform.localPosition = _defaultLocalPos;
        }

        private void OnLedgeCollision(bool enableLedgeGrab, int ledgeID, Transform ledgeTransform)
        {
            onLedgeCollision?.Invoke(enableLedgeGrab, ledgeID, ledgeTransform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FrontLedge"))
            {
                OnLedgeCollision(true, 0, other.transform.parent);
            }
            else if (other.CompareTag("RearLedge"))
            {
                OnLedgeCollision(true, 1, other.transform.parent);
            }
        }
    }
}

