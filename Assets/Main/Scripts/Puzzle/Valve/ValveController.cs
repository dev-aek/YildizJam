using Cinemachine;
using EventBus;
using EventBus.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puzzle.Valve
{
    public class ValveController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform valve;
        [SerializeField] private Transform indicator;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private PlayerInput playerInput;

        [Space(10)]
        [Header("Properties")]
        [SerializeField] private float correctAngle;
        [SerializeField] private float tolerance;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float rotationSpeed = 5f;
        
        private bool _canRotate;
        private Vector2 _rotationInput;
        private bool _valveCompleted;
        private CinemachineBrain _cinemachineBrain;

        private void Awake()
        {
            _cinemachineBrain = UnityEngine.Camera.main.GetComponent<CinemachineBrain>();
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
            _rotationInput = obj.ReadValue<Vector2>();

            if (obj.phase == InputActionPhase.Canceled)
            {
                float angleDifference = Quaternion.Angle(valve.localRotation, Quaternion.Euler(0, correctAngle, 0));

                if (angleDifference <= tolerance && !_valveCompleted)
                {
                    _valveCompleted = true;
                    EventBus<ValveCompletedEvent>.Dispatch(new ValveCompletedEvent { IsCompleted = true });
                }
                else if (_valveCompleted)
                {
                    _valveCompleted = false;
                    EventBus<ValveCompletedEvent>.Dispatch(new ValveCompletedEvent { IsCompleted = false });
                }
            }
        }

        private void FixedUpdate()
        {
            if (_canRotate)
            {
                RotateValve();
            }
        }

        private void RotateValve()
        {
            if(_rotationInput.magnitude >= 0.1f)
            {
                float angle = Mathf.Atan2(_rotationInput.x, _rotationInput.y) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, minAngle, maxAngle);
                Quaternion targetRotation = Quaternion.Euler(0, -angle, 0);
                
                valve.localRotation = Quaternion.Lerp(valve.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
                indicator.localRotation = Quaternion.Lerp(indicator.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
