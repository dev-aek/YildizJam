using Atmosfer;

namespace EventBus.Events
{
    public struct InteractionTutorialEvent
    {
        public LevelEnum PuzzleLevel;
        public bool IsShow;
    }
}