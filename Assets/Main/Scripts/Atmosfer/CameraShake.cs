using Cinemachine;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Atmosfer
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineImpulseSource impulse;

        private void Awake()
        {
            impulse = transform.GetComponent<CinemachineImpulseSource>();
        }
        private void ShakeCamera(ShakeActionEvent @event)
        {
            if (@event.IsShaked)
            {
                impulse.GenerateImpulse();
            }
        }

        private void OnEnable()
        {
            EventBus<ShakeActionEvent>.Subscribe(ShakeCamera);
        }

        private void OnDisable()
        {
            EventBus<ShakeActionEvent>.Unsubscribe(ShakeCamera);
        }

    }
}
