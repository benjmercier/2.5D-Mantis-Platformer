using System;
using System.Collections;
using System.Collections.Generic;
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

        public static event Action<Rigidbody, GameObject> onAttachToRope;

        private void OnEnable()
        {
            PlayerControllerFSM.onDetachFromRope += DetachFromRope;
        }

        private void OnDisable()
        {
            PlayerControllerFSM.onDetachFromRope -= DetachFromRope;
        }

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

