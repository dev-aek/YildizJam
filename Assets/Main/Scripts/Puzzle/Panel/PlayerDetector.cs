using Atmosfer;
using EventBus;
using EventBus.Events;
using Player;
using UnityEngine;

namespace Puzzle.Panel
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialUI;
        [SerializeField] private PanelManager panelManager;

        private void Start()
        {
            tutorialUI.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tutorialUI.SetActive(true);
                Debug.Log("Player detected!");
                EventBus<ChangeCurrentLevelEvent>.Dispatch(new ChangeCurrentLevelEvent{CurrentLevel = LevelEnum.Panel});
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent()
                    { State = PlayerState.Interact });
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent { PanelManager = panelManager });
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tutorialUI.SetActive(false);
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent() { State = PlayerState.Walk });
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent { PanelManager = panelManager });
            }
        }
    }
}