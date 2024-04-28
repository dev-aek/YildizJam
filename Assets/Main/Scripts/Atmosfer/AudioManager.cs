using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Atmosfer
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource[] effects;
        [SerializeField] private AudioSource[] musics;

        private void OnEnable()
        {
            EventBus<ShakeActionEvent>.Subscribe(ExplosionCameraShake);
        }

        private void OnDisable()
        {
            EventBus<ShakeActionEvent>.Unsubscribe(ExplosionCameraShake);
        }

        public void ExplosionCameraShake(ShakeActionEvent @event)
        {
            if (@event.IsPlaySound)
            {
                effects[0].Play();
            }
        }
    }
}
