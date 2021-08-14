using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers.Unity
{
    [DisallowMultipleComponent]
    public sealed class AwakeTrigger : FoundationTriggerBase
    {
        private void Awake()
        {
            this.Trigger();
        }
    }
}