using System;
using System.Collections;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 328966ead566e9361999c135b9ff633a7ed2515f
using UnityEngine;
using Mantis.Scripts.Player.Controller;

namespace Mantis.Scripts.Checkers
{
    public class RopeGrabCheckerFSM : MonoBehaviour
    {
        [SerializeField]
        private bool _isAttachedToRope = false;

        [SerializeField]
        private Rigidbody _robeGrabRB;
        [SerializeField]
        private HingeJoint _ropeGrabHJ;
        [SerializeField]
        private GameObject _ropeGrabPos;

        [SerializeField]
        private Transform _attachToTransform;
        private GameObject _detachFromObj;
        private GameObject _attachedRopeObj;
        private Rigidbody _attachedRopeRB;

        private WaitForSeconds _detachFromWait = new WaitForSeconds(1.5f);

<<<<<<< HEAD
        private Vector3 _defaultLocalPos;

        public static event Action<Rigidbody, GameObject> onAttachToRope;

=======
        public static event Action<Rigidbody, GameObject> onAttachToRope;

        private void OnEnable()
        {
            PlayerControllerFSM.onDetachFromRope += DetachFromRope;
        }

        private void OnDisable()
        {
            PlayerControllerFSM.onDetachFromRope -= DetachFromRope;
        }

>>>>>>> 328966ead566e9361999c135b9ff633a7ed2515f
        private void Start()
        {
            if (_robeGrabRB == null)
            {
                TryGetComponent(out _robeGrabRB);
            }

            if (_ropeGrabHJ == null)
            {
                TryGetComponent(out _ropeGrabHJ);
            }
<<<<<<< HEAD

            _defaultLocalPos = transform.localPosition;
        }

        private void OnEnable()
        {
            PlayerControllerFSM.onDetachFromRope += DetachFromRope;
            PlayerControllerFSM.onDetachFromRope += ResetPosition;
        }

        private void OnDisable()
        {
            PlayerControllerFSM.onDetachFromRope -= DetachFromRope;
            PlayerControllerFSM.onDetachFromRope -= ResetPosition;
=======
>>>>>>> 328966ead566e9361999c135b9ff633a7ed2515f
        }

        private void Update()
        {
            if (!_isAttachedToRope)
            {
                _ropeGrabHJ.connectedAnchor = _ropeGrabPos.transform.position;
            }
        }

        private void AttachToRope(Rigidbody ropeRB)
        {
            _isAttachedToRope = true;
            
            OnAttachToRope(_robeGrabRB, _attachedRopeObj);

            _ropeGrabHJ.connectedBody = ropeRB;
            _ropeGrabHJ.connectedAnchor = Vector3.zero;

            _attachToTransform = ropeRB.gameObject.transform.parent;
        }

        private void OnAttachToRope(Rigidbody ropeGrabRB, GameObject attachedSegment)
        {
            onAttachToRope?.Invoke(ropeGrabRB, attachedSegment);
        }

        private void DetachFromRope()
        {
            _isAttachedToRope = false;

            _detachFromObj = _attachToTransform.transform.gameObject;
            _attachToTransform = null;
            _attachedRopeObj = null;
            _ropeGrabHJ.connectedBody = null;

            StartCoroutine(DetachFromRopeRoutine());
        }

        private IEnumerator DetachFromRopeRoutine()
        {
            yield return _detachFromWait;

            _detachFromObj = null;
        }

<<<<<<< HEAD
        private void ResetPosition()
        {
            transform.localPosition = _defaultLocalPos;
        }

=======
>>>>>>> 328966ead566e9361999c135b9ff633a7ed2515f
        private void OnTriggerEnter(Collider other)
        {
            if (!_isAttachedToRope)
            {
                if (other.CompareTag("Rope"))
                {
                    if (_attachToTransform != other.gameObject.transform.parent)
                    {
                        if (_detachFromObj == null || other.gameObject.transform.parent.gameObject != _detachFromObj)
                        {
                            if (other.gameObject.TryGetComponent(out _attachedRopeRB))
                            {
                                _attachedRopeObj = other.gameObject;

                                AttachToRope(_attachedRopeRB);
                            }
                        }
                    }
                }
            }
        }
    }
}

