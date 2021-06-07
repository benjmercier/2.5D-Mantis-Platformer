using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mantis.Scripts.Checkers
{
    public class RopeChecker : MonoBehaviour
    {
        private bool _isAttachedToRope = false;

        [SerializeField]
        private Rigidbody _ropeCheckerRB;
        [SerializeField]
        private HingeJoint _ropeCheckerHJ;

        [SerializeField]
        private GameObject _ropeGrabPos;
        private GameObject _attachedRopeSegment;

        private Vector3 _swingVector;
        [SerializeField]
        private float _swingForce = 35f;

        private Transform _attachToSegment;
        private GameObject _detachFromSegment;

        private void Start()
        {
            
        }

        private void Update()
        {
            if (!_isAttachedToRope)
            {
                ConnectAnchorToPos(_ropeGrabPos.transform.position);
            }
            else
            {
                CalculateRopeSwing();

                if (_attachedRopeSegment != null)
                {
                    //CalculatePosition();
                }
            }
        }

        private void ConnectAnchorToPos(Vector3 pos)
        {
            _ropeCheckerHJ.connectedAnchor = pos;
        }

        private void CalculateRopeSwing()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _ropeCheckerRB.AddRelativeForce(_swingVector * _swingForce);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _ropeCheckerRB.AddRelativeForce(-_swingVector * _swingForce);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DetachFromRope();
            }
        }

        private void CalculatePosition(GameObject player)
        {
            player.transform.position = _attachedRopeSegment.transform.position;
            player.transform.rotation = Quaternion.Euler(_attachedRopeSegment.transform.localEulerAngles.z,
                player.transform.localEulerAngles.y, 0);
        }

        private void AttachToRopeSegment(Rigidbody ropeRB)
        {
            _ropeCheckerHJ.connectedBody = ropeRB;
            _ropeCheckerHJ.connectedAnchor = Vector3.zero;

            _attachToSegment = ropeRB.gameObject.transform.parent;
        }

        private void DetachFromRope()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isAttachedToRope)
            {
                if (other.CompareTag("Rope"))
                {

                }
            }
        }
    }
}

