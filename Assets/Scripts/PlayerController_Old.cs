using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mantis.Scripts.Checkers;

public class PlayerController_Old : MonoBehaviour
{
    [SerializeField]
    private CharacterController _controller;

    [SerializeField]
    private GameObject _levelStart;

    private float _horInput;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Quaternion _lookRotation;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpHeight = 10f;
    [SerializeField]
    private float _doubleJumpMagnitude = 0.5f;
    private float _doubleJumpHeight;

    private float _yVelocity = 0f;
    private float _maxYVelocity = 1f;

    private float _halfControllerHeight;
    [SerializeField]
    private float _raycastToGroundDistance = 0.2f;

    private bool _canDoubleJump = false;
    private bool _isFalling = false;
    private bool _canFallJump = false;
    private bool _canWallJump = false;

    public bool _isAttachedToRope = false;

    private Vector3 _wallJumpVelocity;

    [SerializeField]
    private Animator _playerAnimator;

    private bool _canClimbUpLedge = false;
    private int _ledgeCheck; // 0 = front, 1 = rear

    // start pos = 3, 1, -1.5




    // Ground Checker
    [SerializeField]
    private GroundChecker _groundChecker;

    // Rope Checker



    // Start is called before the first frame update
    void Start()
    {
        transform.position = _levelStart.transform.position;

        _halfControllerHeight = _controller.height / 2;

        if (_controller == null)
        {
            _controller = GetComponent<CharacterController>();
        }

        if (_playerAnimator == null)
        {
            _playerAnimator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canClimbUpLedge)
        {
            ClimbUpLedge();
        }
        else
        {
            CalculateMovement();
            
            _groundChecker.CheckIfGrounded(_controller, _halfControllerHeight, _canDoubleJump);
        }
    }

    private void CalculateLookRotation()
    {
        _lookRotation = transform.localRotation;
        
        _lookRotation.SetLookRotation(_velocity);
        
        transform.localRotation = _lookRotation;

        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    private void CalculateMovement()
    {
        _horInput = Input.GetAxisRaw("Horizontal");
        
        if (_controller.isGrounded)
        {
            if (_canDoubleJump)
            {
                _canDoubleJump = false;
            }

            if (_canWallJump)
            {
                _canWallJump = false;
            }

            _direction = Vector3.right * _horInput;
            _velocity = _direction * _speed;

            if (_direction != Vector3.zero)
            {
                CalculateLookRotation();
            }

            if (Mathf.Abs(_yVelocity) > _maxYVelocity)
            {
                _yVelocity = _maxYVelocity * Mathf.Sign(_yVelocity);
            }

            _playerAnimator.SetFloat("Speed", Mathf.Abs(_horInput));

            if (_playerAnimator.GetBool("IsJumping") == true || _playerAnimator.GetBool("IsRolling") == true)
            {
                _playerAnimator.SetBool("IsJumping", false);
                _playerAnimator.SetBool("IsRolling", false);
            }

            if (_isFalling)
            {
                _isFalling = false;
                _canFallJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CalculateJump();
            }
        }
        else
        {
            if (!_isFalling)
            {
                _isFalling = true;
            }

            if (_controller.enabled)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CalculateJump();
                }

                _yVelocity += _gravity * Time.deltaTime;

                _playerAnimator.SetFloat("Speed", 0f);
            }
        }
        
        _velocity.y = _yVelocity;

        _playerAnimator.SetFloat("yVelocity", _yVelocity);

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void CalculateJump()
    {
        // jump from ground
        if (_controller.isGrounded)
        {
            _yVelocity = Mathf.Sqrt(_jumpHeight * -2 * _gravity);

            _playerAnimator.SetBool("IsJumping", true);

            _canDoubleJump = true;
        }

        // double jump
        if (_canDoubleJump && !_controller.isGrounded && !_canWallJump)
        {
            _doubleJumpHeight = _jumpHeight * _doubleJumpMagnitude;

            _yVelocity = Mathf.Sqrt(_doubleJumpHeight * -2 * _gravity);

            _playerAnimator.SetBool("IsRolling", true);

            _canDoubleJump = false;
        }

        // jump from running off ledge
        if (!_canDoubleJump && _isFalling && _canFallJump)
        {
            _doubleJumpHeight = _jumpHeight * _doubleJumpMagnitude;

            _yVelocity = Mathf.Sqrt(_doubleJumpHeight * -2 * _gravity);

            _playerAnimator.SetBool("IsJumping", true);

            _canFallJump = false;
        }

        // wall jump
        if (_canWallJump)
        {
            _yVelocity = Mathf.Sqrt(_jumpHeight * -2 * _gravity) / 1.1f;

            _velocity = _wallJumpVelocity;

            if (_velocity != Vector3.zero)
            {
                CalculateLookRotation();
            }

            _canWallJump = false;
        }
    }

    public void GrabLedge(int ledge)
    {
        _ledgeCheck = ledge;

        ToggleCharacterControllerState();

        _playerAnimator.ResetTrigger("LedgeGrab"); 
        _playerAnimator.SetTrigger("LedgeGrab");

        _playerAnimator.SetBool("IsJumping", false);
        _playerAnimator.SetBool("IsRolling", false);

        _canClimbUpLedge = true;
    }

    private void ClimbUpLedge()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _playerAnimator.ResetTrigger("ClimbUp");
            _playerAnimator.SetTrigger("ClimbUp");

            _canClimbUpLedge = false;
        }
    }

    public void CompleteLedgeClimb()
    {
        Vector3 endPos = new Vector3();

        switch (_ledgeCheck)
        {
            case 0:
                endPos.x = 0.75f;
                endPos.y = 0.79f;
                endPos.z = transform.localPosition.z;

                transform.localPosition = endPos;
                break;

            case 1:
                endPos.x = -1.3f;
                endPos.y = 0.79f;
                endPos.z = transform.localPosition.z;

                transform.localPosition = endPos;
                break;
        }

        _velocity = Vector3.zero;
        _yVelocity = 0f;

        transform.SetParent(null);

        ToggleCharacterControllerState();
    }

    public void PlayerOnRope(bool isOnRope)
    {
        _playerAnimator.SetBool("OnRope", isOnRope);

        _isAttachedToRope = isOnRope;
    }

    public bool ReturnCharacterControllerState()
    {
        return _controller.enabled;
    }

    public bool ToggleCharacterControllerState()
    {
        return _controller.enabled = !_controller.enabled;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _wallJumpVelocity = Vector3.zero;

        if (!_controller.isGrounded && hit.transform.CompareTag("Wall"))
        {
            float b = 1f;
            Vector3 V = _velocity;
            Vector3 N = hit.normal;

            _wallJumpVelocity = b * (-2 * Vector3.Dot(V, N) * N + V);

            _canWallJump = true;
        }
    }
}
