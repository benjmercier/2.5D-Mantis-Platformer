using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mantis.Scripts.Checkers
{
    public class RopeGrabChecker : MonoBehaviour
    {
        [SerializeField]
        private PlayerController_Old _player;

        

        [SerializeField]
        private GameObject _ropeGrabPos;
        private Vector3 _defaultPos = new Vector3(0, 1.25f, 0.65f);

        [SerializeField]
        private float _swingForce = 10f;

        private Quaternion _playerRotation;
        private Vector3 _playerSwingVector;

        
        private Transform _attachTo;
        private GameObject _detacthFrom;


        //
        private bool _isAttachedToRope = false;

        [SerializeField]
        private Rigidbody _ropeGrabRB;
        [SerializeField]
        private HingeJoint _ropeGrabHJ;

        private GameObject _attachedRopeSegment;

        //

        // Start is called before the first frame update
        void Start()
        {
            if (_player == null)
            {
                _player = GetComponentInParent<PlayerController_Old>();
            }

            _playerRotation = _player.transform.rotation;
            _playerSwingVector = _player.transform.right;

            if (_ropeGrabRB == null || _ropeGrabHJ == null)
            {
                _ropeGrabRB = GetComponent<Rigidbody>();
                _ropeGrabHJ = GetComponent<HingeJoint>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (!_isAttachedToRope)
            {
                ConnnectAnchorToPos(_ropeGrabPos.transform.position);
            }
            else
            {
                CalculateRopeSwing();

                if (_attachedRopeSegment != null)
                {
                    CalculatePosition();
                }
            }
            */




            if (!_player._isAttachedToRope)
            {
                _ropeGrabHJ.connectedAnchor = _ropeGrabPos.transform.position;
            }
            else
            {
                CalculateMovementOnRope();
                if (_attachedRopeSegment != null)
                {
                    _player.transform.position = _attachedRopeSegment.transform.position;

                    //Vector3 playerPos = _attachedRopeSegment.transform.position; 
                    //playerPos.y -= 1.5f;
                    //_player.transform.position = playerPos;

                    _player.transform.rotation = Quaternion.Euler(-_attachedRopeSegment.transform.localEulerAngles.z, _player.transform.localEulerAngles.y, 0);// _player.transform.eulerAngles.z);
                }
            }
        }

        //
        private void ConnnectAnchorToPos(Vector3 pos)
        {
            _ropeGrabHJ.connectedAnchor = pos;
        }

        private void CalculateRopeSwing()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _ropeGrabRB.AddRelativeForce(_playerSwingVector * _swingForce);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _ropeGrabRB.AddRelativeForce(-_playerSwingVector * _swingForce);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DetachFromRope();
            }
        }

        private void CalculatePosition(GameObject player)
        {
            player.transform.position = _attachedRopeSegment.transform.position;
            player.transform.rotation = Quaternion.Euler(_attachedRopeSegment.transform.localEulerAngles.z, player.transform.localEulerAngles.y, 0);
        }

        //

        private void CalculateMovementOnRope()
        {
            /*
            _horInput = Input.GetAxis("Horizontal");
            //_verInput = Input.GetAxis("Vertical");

            float swing = _horInput * _swingForce;
            //float yForce = _verInput * _swingForce;d

            _ropeGrabRB.AddRelativeForce(0, 0, swing);
            */



            if (Input.GetKey(KeyCode.A))
            {
                _ropeGrabRB.AddRelativeForce(_playerSwingVector * _swingForce);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //_ropeGrabRB.AddRelativeForce(Vector3.back * _swingForce);
                _ropeGrabRB.AddRelativeForce(-_playerSwingVector * _swingForce);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DetachFromRope();
            }
        }

        private void AttachToRope(Rigidbody ropeRB)
        {
            _player.ToggleCharacterControllerState();
            //_player._isAttachedToRope = true;
            _player.PlayerOnRope(true);

            _ropeGrabHJ.connectedBody = ropeRB;

            _ropeGrabHJ.connectedAnchor = Vector3.zero;

            _attachTo = ropeRB.gameObject.transform.parent;
        }

        private void DetachFromRope()
        {
            _player.ToggleCharacterControllerState();
            //_player._isAttachedToRope = false;
            _player.PlayerOnRope(false);

            _player.transform.rotation = _playerRotation;
            _detacthFrom = _attachTo.gameObject;
            _attachTo = null;
            _attachedRopeSegment = null;
            _ropeGrabHJ.connectedBody = null;

            StartCoroutine(DetachFromRopeRoutine());
        }

        IEnumerator DetachFromRopeRoutine()
        {
            yield return new WaitForSeconds(1.5f);

            _detacthFrom = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_player._isAttachedToRope)
            {
                if (other.CompareTag("Rope"))
                {
                    if (_attachTo != other.gameObject.transform.parent)
                    {
                        if (_detacthFrom == null || other.gameObject.transform.parent.gameObject != _detacthFrom)
                        {
                            // attach
                            AttachToRope(other.gameObject.GetComponent<Rigidbody>());
                            _attachedRopeSegment = other.gameObject;
                        }
                    }
                }
            }
        }
    }
}

