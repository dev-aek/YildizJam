using Atmosfer;
using EventBus;
using EventBus.Events;
using Puzzle.Light;
using Puzzle.Panel;
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
        private LightController _lightController;
        private PanelManager _panelManager;
        private PlayerState _currentState;
        private LevelEnum _currentLevel;
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
                InteractObject(_currentLevel, true);
                _currentState = PlayerState.Puzzle;
            }
        }

        public void OnRun(InputAction.CallbackContext obj)
        {
            if (obj.started)
            {
                speed *= 1.5f;
            }
            else if (obj.canceled)
            {
                speed /= 1.5f;
            }
        }
        
        public void OnCancelInteract(InputAction.CallbackContext obj)
        {
            if (obj.started && _currentState == PlayerState.Puzzle)
            {
                InteractObject(_currentLevel, false);
                _currentState = PlayerState.Interact;
            }
        }

        private void OnEnable()
        {
            EventBus<PlayerDetectedEvent>.Subscribe(SetValveController);
            EventBus<ChangeCurrentLevelEvent>.Subscribe(SetCurrentLevel);
        }
        
        private void OnDisable()
        {
            EventBus<PlayerDetectedEvent>.Unsubscribe(SetValveController);
            EventBus<ChangeCurrentLevelEvent>.Unsubscribe(SetCurrentLevel);
        }

        private void SetCurrentLevel(ChangeCurrentLevelEvent @event)
        {
            _currentLevel = @event.CurrentLevel;
        }

        private void SetValveController(PlayerDetectedEvent @event)
        {
            _valveController = @event.ValveController;
            _lightController = @event.LightController;
            _panelManager = @event.PanelManager;
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
        
        private void InteractObject(LevelEnum level, bool isInteract)
        {
            if (level == LevelEnum.Valve)
            {
                _valveController.SetInteract(isInteract);
            }
            else if (level == LevelEnum.Light)
            {
                _lightController.SetInteract(isInteract);
            }
            else if (level == LevelEnum.Panel)
            {
                _panelManager.SetInteract(isInteract);
            }
        }
        
    }
}