using DG.Tweening;
using UnityEngine;

namespace Puzzle.Panel.Motherboard
{
    public class BoardDragAndDrop : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        private Vector3 initialPosition;
        private Vector3 offset;
        private bool isDragging;
        private Transform targetTransform;
        private bool isReturning;

        private void OnMouseDown()
        {
            if (!isDragging && !isReturning)
            {
                initialPosition = transform.position;
                offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                isDragging = true;
            }
        }

        private void OnMouseDrag()
        {
            Vector3 touchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(touchPosition) + offset;

            transform.position = newPosition;
        }

        private void OnMouseUp()
        {
            if (isDragging)
            {
                isDragging = false;
                CheckDropTarget();
                if (targetTransform == null)
                {
                    _collider.enabled = false;
                    transform.DOMove(initialPosition, 1f).OnComplete(() =>
                    {
                        isReturning = false;
                        _collider.enabled = true;
                    });
                }
            }
        }

        private void CheckDropTarget()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                targetTransform = hit.collider.transform;
                if (hit.collider.CompareTag("placeholder"))
                {
                    targetTransform = hit.collider.transform;
                    transform.DOMove(targetTransform.position, 0.5f);
                    transform.DORotate(targetTransform.rotation.eulerAngles, 0.5f).OnComplete(() =>
                    {
                        Destroy(_collider);
                        Destroy(this);
                    });
                }
                else
                {
                    targetTransform = null;
                    isReturning = true;
                }
            }
            else
            {
                targetTransform = null;
                isReturning = true;
            }
        }
    }
}