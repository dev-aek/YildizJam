using EventBus;
using EventBus.Events;
using Puzzle.Valve;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform targetCamera;
        [SerializeField] private LayerMask layer;
        
        [Space(10)] 
        [Header("Properties")] 
        [SerializeField] private float speed = 6f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float gravity;
        [SerializeField] private bool isGrounded;
        [SerializeField] private float groudRadius;

        private float _turnSmoothVelocity;
        private Vector3 _velocity;
        private Vector3 _moveDir;
        private ValveController _valveController;
        private PlayerState _currentState;

        public PlayerState CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        // public ParticleSystem dust;

        public void OnMove(InputAction.CallbackContext obj)
        {
            _moveDir = new Vector3(obj.ReadValue<Vector2>().x, 0f, obj.ReadValue<Vector2>().y).normalized;
        }
        
        public void OnInteract(InputAction.CallbackContext obj)
        {
            if (obj.started && _currentState == PlayerState.Interact)
            {
                Debug.Log("Interact!");
                _valveController.SetInteract(true);
                _currentState = PlayerState.Puzzle;
            }
        }
        
        public void OnCancelInteract(InputAction.CallbackContext obj)
        {
            if (obj.started && _currentState == PlayerState.Puzzle)
            {
                _valveController.SetInteract(false);
                _currentState = PlayerState.Interact;
            }
        }

        private void OnEnable()
        {
            EventBus<PlayerDetectedEvent>.Subscribe(SetValveController);
        }
        
        private void OnDisable()
        {
            EventBus<PlayerDetectedEvent>.Unsubscribe(SetValveController);
        }

        private void SetValveController(PlayerDetectedEvent @event)
        {
            _valveController = @event.ValveController;
        }

        private void FixedUpdate()
        {
            if (_currentState is PlayerState.Walk or PlayerState.Interact)
            {
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            if (_moveDir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(_moveDir.x, _moveDir.z) * Mathf.Rad2Deg + targetCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(10f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * (speed * Time.deltaTime));
                //dust.Play();
            }
            else
            {
                //dust.Stop();
            }

            if (_velocity.y > -20)
            {
                _velocity.y += (gravity * 10) * Time.deltaTime;
            }
            controller.Move(_velocity * Time.deltaTime);
            
        }
        public void OnJump(InputAction.CallbackContext obj)
        {
            if (obj.started && _currentState == PlayerState.Walk || _currentState == PlayerState.Interact)
            {
                Jump();
            }
        }

        public void OnRun(InputAction.CallbackContext obj)
        {
            if (obj.started && _currentState == PlayerState.Walk || _currentState == PlayerState.Interact)
            {
                Run();
            }
        }

        private void Jump()
        {
            isGrounded = Physics.CheckSphere(transform.position, groudRadius, layer);

            if (isGrounded && _velocity.y < 0)
            {
                _velocity.y = -1;
            }
            if (isGrounded)
            {
                _velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
            }
        }

        private void Run()
        {
            Debug.Log("Run");
        }
    }
}