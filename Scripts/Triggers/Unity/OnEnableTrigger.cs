using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers.Unity
{
    [DisallowMultipleComponent]
    public sealed class OnEnableTrigger : FoundationTriggerBase
    {
        private void OnEnable()
        {
            this.Trigger();
        }
    }
}