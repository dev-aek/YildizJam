using Cinemachine;
using EventBus;
using EventBus.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puzzle.Light
{
    public class LightController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform light;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private PlayerInput playerInput;
        
        
        [Space(10)]
        [Header("Properties")]
        [SerializeField] private float correctAngle;
        [SerializeField] private float tolerance;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float rotationSpeed = 5f;
        
        private bool _canRotate = true;
        private Vector2 _rotationInput;
        private bool _lightCompleted;
        private CinemachineBrain _cinemachineBrain;

        private void Awake()
        {
            _cinemachineBrain = UnityEngine.Camera.main.GetComponent<CinemachineBrain>();
        }

        private void Start()
        {
            //_canRotate = false;
            //playerInput.enabled = false;
            virtualCamera.Priority = 11;
        }

        public void SetInteract(bool value)
        {
            _canRotate = value;
            virtualCamera.Priority = value ? 11 : 9;
            playerInput.enabled = value;
        }
        
        public void OnRotate(InputAction.CallbackContext obj)
        {
            if(_cinemachineBrain.IsBlending) return;
            _rotationInput = -obj.ReadValue<Vector2>();

            if (obj.phase == InputActionPhase.Canceled)
            {
                float angleDifference = Quaternion.Angle(light.localRotation, Quaternion.Euler(correctAngle, 0, light.localRotation.z));

                if (angleDifference <= tolerance && !_lightCompleted)
                {
                    _lightCompleted = true;
                    EventBus<LightCompletedEvent>.Dispatch(new LightCompletedEvent{ IsCompleted = true });
                }
                else if (_lightCompleted)
                {
                    _lightCompleted = false;
                    EventBus<LightCompletedEvent>.Dispatch(new LightCompletedEvent{ IsCompleted = false });
                }
            }
        }

        private void FixedUpdate()
        {
            if (_canRotate)
            {
                RotateLight();
            }
        }

        private void RotateLight()
        {
            if(_rotationInput.magnitude >= 0.1f)
            {
                float angle = Mathf.Atan2(_rotationInput.x, _rotationInput.y) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, minAngle, maxAngle);
                Quaternion targetRotation = Quaternion.Euler(angle, 0, light.localRotation.z);
                
                light.localRotation = Quaternion.Lerp(light.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}