using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamMove : MonoBehaviour
{
    public void Update()
    {
        camMove();
    }
    public void camMove()
   {
        transform.DOLocalMove(new Vector3(1f,2f,0), 500f);
   }
}
