using Puzzle.Light;
using Puzzle.Valve;

namespace EventBus.Events
{
    public struct PlayerDetectedEvent
    {
        public ValveController ValveController;
        public LightController LightController;
    }
}