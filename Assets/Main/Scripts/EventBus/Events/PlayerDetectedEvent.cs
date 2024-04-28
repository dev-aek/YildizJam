using Puzzle.Light;
using Puzzle.Panel;
using Puzzle.Valve;

namespace EventBus.Events
{
    public struct PlayerDetectedEvent
    {
        public PanelManager PanelManager;
        public ValveController ValveController;
        public LightController LightController;
    }
}