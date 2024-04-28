using DG.Tweening;
using UnityEngine;

namespace Puzzle.Panel.Screw
{
    public class BoxController : MonoBehaviour
    {
        [SerializeField] private Transform boxTransform;
        private float _screwCount = 4;
        
        public void DecreaseScrewCount()
        {
            _screwCount--;
            if (_screwCount == 0)
            {
                boxTransform.DOLocalMoveY(0, 0.7f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            }
        }
    }
}