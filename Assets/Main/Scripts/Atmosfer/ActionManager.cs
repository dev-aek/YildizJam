using System.Collections;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Atmosfer
{
    public class ActionManager : MonoBehaviour
    {
        [SerializeField] private float shakeEventTime;
        [SerializeField] private Light[] lights;

        private void Start()
        {
            StartCoroutine(StartShakeEvent());

        }

        IEnumerator StartShakeEvent()
        {
            yield return new WaitForSeconds(shakeEventTime);
            EventBus<ShakeActionEvent>.Dispatch(new ShakeActionEvent { IsShaked = true, IsPlaySound = true });
            StartCoroutine(LightClose());
            StartCoroutine(StartShakeEvent());

        }

        IEnumerator LightClose()
        {
            foreach (var light in lights)
            {
                light.intensity = 0;
            }
            yield return new WaitForSeconds(1);
            foreach (var light in lights)
            {
                light.intensity = 200;
            }
        }

    }
}
