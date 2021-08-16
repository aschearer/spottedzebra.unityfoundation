#if NODE_CANVAS
using NodeCanvas.Framework;
using ParadoxNotion;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Tasks.Variables
{
    public sealed class WaitForGameEvent : ActionTask
    {
        public BBParameter<GameEvent> GameEvent;
        
        public BBParameter<CompactStatus> FinishStatus = new BBParameter<CompactStatus>(CompactStatus.Success);

        protected override void OnExecute()
        {
            if (this.GameEvent.isNoneOrNull)
            {
                // no game event defined
                this.EndAction(false);
            }
            else
            {
                this.GameEvent.value.RegisterListener(this.OnGameEventRaised);
            }
        }

        private void OnGameEventRaised()
        {
            this.GameEvent.value.UnregisterListener(this.OnGameEventRaised);
            this.EndAction(this.FinishStatus.value == CompactStatus.Success);
        }
    }
}
#endif