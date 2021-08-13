namespace SpottedZebra.UnityFoundation.Triggers
{
    public class OnEnableTrigger : FoundationTriggerBase
    {
        private void OnEnable()
        {
            this.Trigger();
        }

        protected override bool OnTrigger()
        {
            return true;
        }
    }
}