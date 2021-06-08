using UnityEngine;

namespace Mantis.Scripts.Checkers
{
    public class LedgeGrabCheckerFSM : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FrontLedge"))
            {
                // invoke event
            }
            else if (other.CompareTag("RearLedge"))
            {
                // invoke event
            }
        }
    }
}

