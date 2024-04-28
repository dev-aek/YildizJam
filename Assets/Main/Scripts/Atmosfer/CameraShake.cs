using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBus;
using EventBus.Events;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineImpulseSource impulse;

    private void Awake()
    {
        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }
    public void ShakeCamera(ShakeActionEvent @event)
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
