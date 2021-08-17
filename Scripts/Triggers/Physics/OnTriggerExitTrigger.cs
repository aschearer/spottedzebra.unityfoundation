using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Extensions;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers.Physics
{
    public sealed class OnTriggerExitTrigger : FoundationTriggerBase
    {
        [SerializeField] private bool filterByTag;

        [ShowIf("filterByTag")] [TagSelector] [SerializeField]
        private string targetTag;
        
        private void OnTriggerExit(Collider other)
        {
            if (!this.filterByTag ||
                other.CompareTag(this.targetTag))
            {
                this.Trigger();
            }
        }
    }
}