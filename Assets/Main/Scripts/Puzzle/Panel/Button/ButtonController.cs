using DG.Tweening;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Puzzle.Panel.Button
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Transform leftPart;
        [SerializeField] private Transform rightPart;
        [SerializeField] private Collider collider;
        

        private void OnEnable()
        {
            EventBus<PanelActivatedEvent>.Subscribe(OnPanelActivation);
        }
        
        private void OnDisable()
        {
            EventBus<PanelActivatedEvent>.Unsubscribe(OnPanelActivation);
        }

        private void OnPanelActivation(PanelActivatedEvent @event)
        {
            var leftPos = leftPart.localPosition;
            leftPart.DOLocalMove(new Vector3(leftPos.x + 0.75f, leftPos.y, leftPos.z), 1f);
            var rightPos = rightPart.localPosition;
            rightPart.DOLocalMove(new Vector3(rightPos.x - 0.75f, rightPos.y, rightPos.z), 1f).OnComplete(() =>
            {
                ActivateButton();
            });
        }

        private void ActivateButton()
        {
            collider.enabled = true;
        }
    }
}