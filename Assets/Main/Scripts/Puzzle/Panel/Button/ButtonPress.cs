using DG.Tweening;
using UnityEngine;

namespace Puzzle.Panel.Button
{
    public class ButtonPress : MonoBehaviour
    {
        [SerializeField] private Collider collider;
        private void OnMouseDown()
        {
            transform.DOLocalMoveY(transform.localPosition.y - 0.3f, 0.4f).OnComplete((() =>
            {
                transform.DOLocalMoveY(transform.localPosition.y + 0.3f, 0.4f).OnComplete(() =>
                {
                    collider.enabled = false;
                });
            }));
        }
    }
}