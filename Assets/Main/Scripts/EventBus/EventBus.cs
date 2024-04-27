using System.Collections.Generic;

namespace EventBus
{
    public static class EventBus<TEvent>
    {
        private static readonly List<EventListener<TEvent>> _listeners = new();
        private static readonly object _lock = new();

        public static void Subscribe(EventListener<TEvent> listener)
        {
            lock (_lock)
            {
                if (!_listeners.Contains(listener))
                {
                    _listeners.Add(listener);
                }
            }
        }

        public static void Unsubscribe(EventListener<TEvent> listener)
        {
            lock (_lock)
            {
                _listeners.Remove(listener);
            }
        }

        public static void Dispatch(TEvent eventToDispatch)
        {
            List<EventListener<TEvent>> listenersSnapshot;
            lock (_lock)
            {
                listenersSnapshot = new List<EventListener<TEvent>>(_listeners);
            }

            foreach (var listener in listenersSnapshot)
            {
                listener(eventToDispatch);
            }
        }
    }
}