using System;
using Atmosfer;
using EventBus.Events;
using EventBus;
using Player;
using UnityEngine;

namespace Puzzle.Light
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialUI;
        [SerializeField] private LightController lightController;

        private void Start()
        {
            tutorialUI.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tutorialUI.SetActive(true);
                EventBus<ChangeCurrentLevelEvent>.Dispatch(new ChangeCurrentLevelEvent{CurrentLevel = LevelEnum.Light});
                Debug.Log("Player detected!");
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent{State = PlayerState.Interact});
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent{LightController = lightController});
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tutorialUI.SetActive(false);
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent(){State = PlayerState.Walk});
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent{LightController = lightController});
            }
        }
    }
}