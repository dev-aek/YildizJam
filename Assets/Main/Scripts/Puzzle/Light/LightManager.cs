using EventBus.Events;
using EventBus;
using UnityEngine;

namespace Puzzle.Light
{
    public class LightManager : MonoBehaviour
    {
        [SerializeField] private int lightCount;
        
        private int _completedLightCount;
        private void OnEnable()
        {
            EventBus<LightCompletedEvent>.Subscribe(OnLightCompleted);
        }
        
        private void OnDisable()
        {
            EventBus<LightCompletedEvent>.Unsubscribe(OnLightCompleted);
        }

        private void OnLightCompleted(LightCompletedEvent @event)
        {
            if (@event.IsCompleted)
            {
                _completedLightCount++;
            }
            else
            {
                _completedLightCount--;
            }
            
            if (_completedLightCount == lightCount)
            {
                Debug.Log("All lights are completed!");
            }
        }
    }
}