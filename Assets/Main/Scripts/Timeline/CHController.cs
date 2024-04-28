using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHController : MonoBehaviour
{

    public float speed;
    private void Update()
    {

        transform.Translate(-1 * speed, 0, 0);


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CinePoint"))
        {
            Debug.Log("dur");
            speed = 0;
        }

    }
}
