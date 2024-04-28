using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigtAlarm : MonoBehaviour
{
    private Light light;

    private void Awake()
    {
        light = transform.GetComponent<Light>();
    }
    private void Start()
    {
        StartCoroutine(AmergincyAlarm());
    }

    IEnumerator AmergincyAlarm()
    {
        float i = Random.RandomRange(0.5f, 3);
        yield return new WaitForSeconds(i);
        light.intensity = 0;
        yield return new WaitForSeconds(0.5f);
        light.intensity = 200;
        StartCoroutine(AmergincyAlarm());
    }
}
