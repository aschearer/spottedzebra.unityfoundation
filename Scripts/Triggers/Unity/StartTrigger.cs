using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers.Unity
{
    [DisallowMultipleComponent]
    public sealed class StartTrigger : FoundationTriggerBase
    {
        private void Start()
        {
            this.Trigger();
        }
    }
}