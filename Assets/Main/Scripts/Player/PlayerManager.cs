using System;
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
            EventBus<ChangePlayerState>.Subscribe(SetPlayerState);
        }
        
        private void OnDisable()
        {
            EventBus<ChangePlayerState>.Unsubscribe(SetPlayerState);
        }

        private void SetPlayerState(ChangePlayerState @event)
        {
            switch (@event.State)
            {
                case PlayerState.Walk:
                    playerController.CanMove = true;
                    break;
                case PlayerState.Puzzle:
                    playerController.CanMove = false;
                    break;
            }
        }
    }
}