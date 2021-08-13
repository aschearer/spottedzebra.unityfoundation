namespace SpottedZebra.UnityFoundation.Triggers
{
    public class AwakeTrigger : FoundationTriggerBase
    {
        private void Awake()
        {
            this.Trigger();
        }

        protected override bool OnTrigger()
        {
            return true;
        }
    }
}