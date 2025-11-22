using ArtGenerator.Core.Events.EventArgs;
using System;

namespace ArtGenerator.Core.Events.EventHandles {
    public static class Handles {
        public static event Action<RunningRenderEventArg> _runningRenderEvent;
        public static event Action<RunRenderEventArg> _runRenderEvent;
        public static event Action<EndRenderEventArg> _endRenderEvent;
        public static event Action<RemovingRenderEventArg> _removingRenderEvent;
        public static event Action<RemoveRenderEventArg> _removeRenderEvent;

        internal static RunningRenderEventArg OnRunningRenderEvent(RunningRenderEventArg arg) {
            _runningRenderEvent?.Invoke(arg);
            return arg;
        }

        internal static RunRenderEventArg OnRunRenderEvent(RunRenderEventArg arg) {
            _runRenderEvent?.Invoke(arg);
            return arg;
        }

        internal static EndRenderEventArg OnEndRenderEvent(EndRenderEventArg arg) {
            _endRenderEvent?.Invoke(arg);
            return arg;
        }

        internal static RemovingRenderEventArg OnRemovingRenderEventArg(RemovingRenderEventArg arg) { 
            _removingRenderEvent?.Invoke(arg);
            return arg;
        }
        internal static RemoveRenderEventArg OnRemoveRenderEventArg(RemoveRenderEventArg arg) { 
            _removeRenderEvent?.Invoke(arg);
            return arg;
        }
    }
}
