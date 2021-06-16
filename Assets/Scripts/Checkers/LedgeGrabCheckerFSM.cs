using System;
using UnityEngine;

namespace Mantis.Scripts.Checkers
{
    public class LedgeGrabCheckerFSM : MonoBehaviour
    {
        public static event Action<bool, int, Transform> onLedgeCollision;

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

