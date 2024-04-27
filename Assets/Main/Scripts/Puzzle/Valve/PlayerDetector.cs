using EventBus;
using EventBus.Events;
using Player;
using UnityEngine;

namespace Puzzle.Valve
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private ValveController valveController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player detected!");
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent(){State = PlayerState.Interact});
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent{ValveController = valveController});
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventBus<ChangePlayerStateEvent>.Dispatch(new ChangePlayerStateEvent(){State = PlayerState.Walk});
                EventBus<PlayerDetectedEvent>.Dispatch(new PlayerDetectedEvent{ValveController = valveController});
            }
        }
    }
}