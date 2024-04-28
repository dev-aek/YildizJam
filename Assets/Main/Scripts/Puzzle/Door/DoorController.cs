using Atmosfer;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Puzzle.Door
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private LevelEnum levelEnum;
        [SerializeField] private Animator animator;

        private void OnEnable()
        {
            EventBus<OpenDoorEvent>.Subscribe(OpenDoor);
        }
        
        private void OnDisable()
        {
            EventBus<OpenDoorEvent>.Unsubscribe(OpenDoor);
        }

        private void OpenDoor(OpenDoorEvent @event)
        {
            if (@event.LevelEnum == levelEnum)
            {
                animator.SetTrigger("Open");
            }
        }
    }
}