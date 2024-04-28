using System;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Puzzle.Panel.CableManagement
{
    public class Wire : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        [SerializeField] private string targetTag;
        

        private Vector3 offset;

        private void OnMouseDown()
        {
            offset = transform.position - MouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            line.SetPosition(0, MouseWorldPosition() + offset);
            line.SetPosition(1, transform.position);
        }

        private void OnMouseUp()
        {
            var rayOrigin = Camera.main.transform.position;
            var rayDirection = MouseWorldPosition() - rayOrigin;
            RaycastHit hit;
            
            if (Physics.Raycast(rayOrigin, rayDirection, out hit))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    line.SetPosition(0, hit.point);
                    EventBus<CableConnectedEvent>.Dispatch(new CableConnectedEvent());
                }
                else
                {
                    line.SetPosition(0, transform.position);
                }
            }
            else
            {
                line.SetPosition(0, transform.position);
            }
        }

        private Vector3 MouseWorldPosition()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}