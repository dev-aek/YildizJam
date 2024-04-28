using DG.Tweening;
using UnityEngine;

namespace Timeline
{
    public class CamMove : MonoBehaviour
    {
        public void Update()
        {
            CameraMove();
        }
        private void CameraMove()
        {
            transform.DOLocalMove(new Vector3(1f,2f,0), 700f);
        }
    }
}
