using Atmosfer;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Puzzle.Valve
{
    public class ValveManager : MonoBehaviour
    {
        [SerializeField] private int valveCount;
        
        private int _completedValveCount;
        private void OnEnable()
        {
            EventBus<ValveCompletedEvent>.Subscribe(OnValveCompleted);
        }
        
        private void OnDisable()
        {
            EventBus<ValveCompletedEvent>.Unsubscribe(OnValveCompleted);
        }

        private void OnValveCompleted(ValveCompletedEvent @event)
        {
            if (@event.IsCompleted)
            {
                _completedValveCount++;
            }
            else
            {
                _completedValveCount--;
            }
            
            if (_completedValveCount == valveCount)
            {
                EventBus<TimeAwardEvent>.Dispatch(new TimeAwardEvent{ Time = 15 });
                EventBus<OpenDoorEvent>.Dispatch(new OpenDoorEvent{LevelEnum = LevelEnum.Valve});
            }
        }
    }
}