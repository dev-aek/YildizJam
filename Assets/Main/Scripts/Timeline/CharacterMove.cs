using UnityEngine;

namespace Timeline
{
<<<<<<< HEAD
    public float speed;
    public GameObject gb;
    private void Update()
=======
    public class CharacterMove : MonoBehaviour
>>>>>>> 3d4509f631557cdc75ec23216c01f12f4c7caa6d
    {
        public float speed;
        private void Update()
        {
<<<<<<< HEAD
            Debug.Log("dur");
            speed = 0;
            gb.SetActive(false);
=======
            transform.Translate(-1*speed, 0, 0);

>>>>>>> 3d4509f631557cdc75ec23216c01f12f4c7caa6d
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
}
