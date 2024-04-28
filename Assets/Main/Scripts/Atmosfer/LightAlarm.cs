using System.Collections;
using UnityEngine;

namespace Atmosfer
{
    public class LightAlarm : MonoBehaviour
    {
        private Light light;

        private void Awake()
        {
            light = transform.GetComponent<Light>();
        }
        private void Start()
        {
            StartCoroutine(EmergencyAlarm());
        }

        IEnumerator EmergencyAlarm()
        {
            float i = Random.RandomRange(0.5f, 3);
            yield return new WaitForSeconds(i);
            light.intensity = 0;
            yield return new WaitForSeconds(0.5f);
            light.intensity = 200;
            StartCoroutine(EmergencyAlarm());
        }
    }
}
