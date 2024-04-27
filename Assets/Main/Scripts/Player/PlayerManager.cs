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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            EventBus<ChangePlayerStateEvent>.Subscribe(SetPlayerState);
        }
        
        private void OnDisable()
        {
            EventBus<ChangePlayerStateEvent>.Unsubscribe(SetPlayerState);
        }
        
        private void SetPlayerState(ChangePlayerStateEvent @event)
        {
            playerController.CurrentState = @event.State;
        }
    }
}