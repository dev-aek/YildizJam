using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBus;
using EventBus.Events;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private float shakeEventTime;
    private void Start()
    {
        StartCoroutine(StartShakeEvent());

    }

    IEnumerator StartShakeEvent()
    {
        yield return new WaitForSeconds(shakeEventTime);
        EventBus<ShakeActionEvent>.Dispatch(new ShakeActionEvent { IsShaked = true, IsPlaySound = true });
        StartCoroutine(StartShakeEvent());

    }


}
