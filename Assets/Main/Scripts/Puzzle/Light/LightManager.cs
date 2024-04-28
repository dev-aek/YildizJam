using Atmosfer;
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
                EventBus<TimeAwardEvent>.Dispatch(new TimeAwardEvent{ Time = 15 });
                EventBus<OpenDoorEvent>.Dispatch(new OpenDoorEvent{LevelEnum = LevelEnum.Light});
            }
        }
    }
}