namespace SpottedZebra.UnityFoundation.Triggers
{
    public class StartTrigger : FoundationTriggerBase
    {
        private void Start()
        {
            this.Trigger();
        }

        protected override bool OnTrigger()
        {
            return true;
        }
    }
}