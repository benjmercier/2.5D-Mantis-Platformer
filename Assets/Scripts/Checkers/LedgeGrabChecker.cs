using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mantis.Scripts.Checkers
{
    public class LedgeGrabChecker : MonoBehaviour
    {
        [SerializeField]
        private PlayerController_Old _player;

        [SerializeField]
        private Vector3 _frontLedgeHandPos = new Vector3(0.03f, -1.75f, -1.5f);
        [SerializeField]
        private Vector3 _rearLedgeHandPos = new Vector3(-0.47f, -1.75f, -1.5f);

        // Start is called before the first frame update
        void Start()
        {
            if (_player == null)
            {
                _player = GetComponentInParent<PlayerController_Old>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FrontLedge"))
            {
                _player.GrabLedge(0);

                _player.gameObject.transform.SetParent(other.transform.parent, false);

                _player.transform.localPosition = _frontLedgeHandPos;
            }
            else if (other.CompareTag("RearLedge"))
            {
                _player.GrabLedge(1);

                _player.gameObject.transform.SetParent(other.transform.parent, false);

                _player.transform.localPosition = _rearLedgeHandPos;
            }
        }
    }
}

