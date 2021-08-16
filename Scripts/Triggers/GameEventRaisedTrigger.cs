using SpottedZebra.UnityFoundation.Extensions.Design;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Triggers
{
    public class GameEventRaisedTrigger : FoundationTriggerBase
    {
        [ListRequired]
        public GameEvent[] GameEvents;

        private void OnEnable()
        {
            foreach (GameEvent gameEvent in this.GameEvents)
            {
                gameEvent.RegisterListener(this.Trigger);
            }
        }

        private void OnDisable()
        {
            foreach (GameEvent gameEvent in this.GameEvents)
            {
                gameEvent.UnregisterListener(this.Trigger);
            }
        }
    }
}