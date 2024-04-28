using UnityEngine;

namespace Puzzle.Panel.Screw
{
    public class ScrewController : MonoBehaviour
    {
        [SerializeField] private BoxController boxController;
        [SerializeField] private Animator screwAnimator;

        private void OnMouseDown()
        {
            screwAnimator.SetTrigger("Screw");
        }

        private void FinishAnim()
        {
            boxController.DecreaseScrewCount();
            Destroy(gameObject);
        }
    }
}