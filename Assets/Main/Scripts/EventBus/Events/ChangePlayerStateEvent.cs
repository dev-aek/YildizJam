using Player;

namespace EventBus.Events
{
    public struct ChangePlayerStateEvent
    {
        public PlayerState State;
    }
}