using System;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Puzzle.Panel.Card
{
    public class CardActivation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

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
            animator.SetTrigger("Activated");
        }

        private void OnMouseDown()
        {
            animator.SetTrigger("Verify");
        }
        
        private void CheckVerification()
        {
            EventBus<CardVerifiedEvent>.Dispatch(new CardVerifiedEvent());
        }
    }
}