using UnityEngine;
using UnityEngine.InputSystem;
using Mantis.InputActions;
using Mantis.Scripts.Player.States;
using Mantis.Scripts.AnimationBehaviors;
using Mantis.Scripts.AnimationParameters;
using Mantis.Scripts.Checkers;

namespace Mantis.Scripts.Player.Controller
{
    public class PlayerControllerFSM : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        private BaseState _currentState;
        public BaseState CurrentState { get { return _currentState; } }

        public readonly IdlingState idlingState = new IdlingState();
        public readonly MovingState movingState = new MovingState();
        public readonly JumpingState jumpingState = new JumpingState();
        public readonly DoubleJumpingState doubleJumpingState = new DoubleJumpingState();
        public readonly FallJumpingState fallJumpingState = new FallJumpingState();
        public readonly WallJumpingState wallJumpingState = new WallJumpingState();
        public readonly FallingState fallingState = new FallingState();
        public readonly LedgeGrabbingState ledgeGrabbingState = new LedgeGrabbingState();
        public readonly LedgeClimbingState ledgeClimbingState = new LedgeClimbingState();

        [SerializeField]
        private CharacterController _controller;
        public CharacterController Controller { get { return _controller; } set { _controller = value; } }

        // Input
        private Vector2 _moveInputContext;
        private float _horizontalInput;
        public float HorizontalInput { get { return _horizontalInput; } }
        private bool _jumpInput = false;
        public bool JumpInput { get { return _jumpInput; } set { _jumpInput = value; } }
        private bool _interactionInput;
        public bool InteractionInput { get { return _interactionInput; } }

        [Header("Gravity")]
        public float groundedGravity = -5f;
        public float fallingGravity = Physics.gravity.y; // -9.81f
        [HideInInspector]
        public float downwardForce;
        public float downwardAcceleration = 5f;

        [Header("Rotation")]
        public Quaternion lookRotation;

        [Header("Movement")]
        [SerializeField]
        private float _maxSpeed = 15f;
        public float MaxSpeed { get { return _maxSpeed; } }
        [SerializeField, Range(0f, 5f)]
        private float _maxForce = 0.25f;
        public float MaxForce { get { return _maxForce; } }

        [HideInInspector]
        public Vector3 targetDirection,
            desiredVelocity,
            steeringVelocity,
            currentVelocity,
            movement;

        [HideInInspector]
        public float previousXPos,
            currentXPos,
            lerpXPos;

        [Header("Jumping")]
        public float jumpHeight = 3.5f;
        [HideInInspector]
        public float jumpVelocity;

        [Header("Double Jumping")]
        public float doubleJumpMagnitude = 0.5f;
        [HideInInspector]
        public float doubleJumpHeight;
        public bool canDoubleJump = false;

        [Header("Fall Jumping")]
        public bool canFallJump = false;

        [Header("Wall Jumping")]
        public bool canWallJump = false;
        [HideInInspector]
        public Vector3 wallJumpVelocity;

        [Header("Ledge Grab")]
        public bool grabLedge = false;
        public int ledgeGrabID; // 0 = front, 1 = rear
        public Transform ledgeParent;
        public Vector3 frontLedgeHandPos = new Vector3(0.03f, -1.75f, -1.5f);
        public Vector3 rearLedgeHandPos = new Vector3(-0.47f, -1.75f, -1.5f);
        public bool canClimbLedge = false;
        public bool ledgeClimbCompleted = false;

        [Header("Animations")]
        [SerializeField]
        private Animator _animator;
        public Animator Animator { get { return _animator; } }
        [SerializeField]
        private PlayerAnimationParameters _animParameters;
        public PlayerAnimationParameters AnimParameters { get { return _animParameters; } }

        private void Awake()
        {
            if (_controller == null)
            {
                if (TryGetComponent(out _controller))
                {
                    Debug.Log("Player::Awake()::_controller reference added.");
                }
                else
                {
                    Debug.LogError("Player::Awake()::_controller is NULL");
                }
            }

            _animParameters = new PlayerAnimationParameters();
        }

        private void OnEnable()
        {
            // register to ledge grab event
            LedgeGrabCheckerFSM.onLedgeCollision += CanGrabLedge;
            LedgeClimbingBehavior.onLedgeClimbCompleted += LedgeClimbCompleted;
        }

        private void OnDisable()
        {
            // deregister from ledge grab event
            LedgeGrabCheckerFSM.onLedgeCollision -= CanGrabLedge;
            LedgeClimbingBehavior.onLedgeClimbCompleted -= LedgeClimbCompleted;
        }

        private void Start()
        {
            TransitionToState(idlingState);
        }

        private void Update()
        {
            _currentState.Update();
            Debug.Log(_currentState);
        }

        public void TransitionToState(BaseState state)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }

            _currentState = state;

            if (_currentState.Player == null)
            {
                _currentState.AssignPlayer(this);
            }

            _currentState.EnterState();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInputContext = context.ReadValue<Vector2>();
            _horizontalInput = _moveInputContext.x;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _jumpInput = context.ReadValue<float>() == 1;
        }

        public void OnInteraction(InputAction.CallbackContext context)
        {
            _interactionInput = context.ReadValue<float>() == 1;
        }

        private void CanGrabLedge(bool enableLedgeGrab, int ledgeID, Transform ledgeTransform)
        {
            grabLedge = enableLedgeGrab;
            ledgeGrabID = ledgeID;
            ledgeParent = ledgeTransform;
        }

        private void LedgeClimbCompleted(bool climbCompleted)
        {
            ledgeClimbCompleted = climbCompleted;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _currentState.OnControllerColliderHit(hit);
        }
    }
}

