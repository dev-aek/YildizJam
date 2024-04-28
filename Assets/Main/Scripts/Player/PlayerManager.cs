using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        private void Start()
        {
            ChangeMouseVisibility(false);
        }

        private void ChangeMouseVisibility(bool isVisible)
        {
            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isVisible;
        }

        private void OnEnable()
        {
            EventBus<ChangePlayerStateEvent>.Subscribe(SetPlayerState);
            EventBus<SetMouseVisibilityEvent>.Subscribe(SetMouseVisibility);
        }
        
        private void OnDisable()
        {
            EventBus<ChangePlayerStateEvent>.Unsubscribe(SetPlayerState);
            EventBus<SetMouseVisibilityEvent>.Unsubscribe(SetMouseVisibility);
        }

        private void SetMouseVisibility(SetMouseVisibilityEvent @event)
        {
            ChangeMouseVisibility(@event.IsVisible);
        }

        private void SetPlayerState(ChangePlayerStateEvent @event)
        {
            playerController.CurrentState = @event.State;
        }
    }
}