using EventBus.Events;
using EventBus;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.Light
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private LightController lightController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player detected!");
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent(){State = PlayerState.Interact});
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent{LightController = lightController});
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent(){State = PlayerState.Walk});
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent{LightController = lightController});
            }
        }
    }
}