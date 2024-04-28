using Atmosfer;
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
        [SerializeField] private Transform joystick;
        [SerializeField] private Transform light;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private GameObject tutorialUI;
        
        [Space(10)]
        [Header("Properties")]
        [SerializeField] private float correctAngle;
        [SerializeField] private float tolerance;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float rotationSpeed = 5f;
        
        private bool _canRotate;
        private Vector2 _rotationInput;
        private bool _lightCompleted;
        private CinemachineBrain _cinemachineBrain;
        private bool _isTimeAdded;

        private void Awake()
        {
            _cinemachineBrain = UnityEngine.Camera.main.GetComponent<CinemachineBrain>();
        }

        public void SetInteract(bool value)
        {
            _canRotate = value;
            virtualCamera.Priority = value ? 11 : 9;
            playerInput.enabled = value;
            tutorialUI.SetActive(!value);
            EventBus<InteractionTutorialEvent>.Dispatch(new InteractionTutorialEvent { PuzzleLevel = LevelEnum.Light, IsShow = value});
        }
        
        public void OnRotate(InputAction.CallbackContext obj)
        {
            if(_cinemachineBrain.IsBlending) return;
            _rotationInput = -obj.ReadValue<Vector2>();

            if (obj.phase == InputActionPhase.Canceled)
            {
                float angleDifference = Quaternion.Angle(light.localRotation, Quaternion.Euler(correctAngle, 0, light.localRotation.z));

                Debug.Log("Angle Difference: " + angleDifference);
                if (angleDifference <= tolerance && !_lightCompleted)
                {
                    if (!_isTimeAdded)
                    {
                        _isTimeAdded = true;
                        EventBus<TimeAwardEvent>.Dispatch(new TimeAwardEvent { Time = 10 });
                    }
                    _canRotate = false;
                    _lightCompleted = true;
                    EventBus<LightCompletedEvent>.Dispatch(new LightCompletedEvent{ IsCompleted = true });
                }
            }
        }

        private void FixedUpdate()
        {
            if (_canRotate)
            {
                RotateLight();
                RotateJoystick();
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
        
        private void RotateJoystick()
        {
            if(_rotationInput.magnitude >= 0.1f)
            {
                float angle = _rotationInput.x < 0 ? 45 : -45;
                Quaternion targetRotation = Quaternion.Euler(angle, 0, 0);
                
                joystick.localRotation = Quaternion.Lerp(joystick.localRotation, targetRotation, 0.5f);
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
                joystick.localRotation = Quaternion.Lerp(joystick.localRotation, targetRotation, 0.5f);
            }
        }
    }
}