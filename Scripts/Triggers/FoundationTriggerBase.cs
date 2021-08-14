using SpottedZebra.UnityFoundation.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Triggers
{
    public abstract class FoundationTriggerBase : MonoBehaviour
    {
        [FoldoutGroup("Notes")] [HideLabel] [Multiline] [Tooltip("For internal purposes only")]
        public string Notes;

        [Tooltip("Decremented every time the trigger fires, stops firing when zero. -1 means run forever.")]
        public IntReference RemainingTriggers = new IntReference(-1);

        [FoldoutGroup("onTriggered")] [HideLabel] [PropertyOrder(1001)] [SerializeField]
        private UnityEvent onTriggered = new UnityEvent();
        
        [PropertyOrder(2000)]
        [Button]
        public void Trigger()
        {
            if (this.RemainingTriggers.Value > 0 || this.RemainingTriggers.Value < 0)
            {
                this.EvaluateTrigger();
            }
        }

        protected virtual void OnTrigger()
        {
        }

        private void EvaluateTrigger()
        {
            if (this.RemainingTriggers.Value > 0)
            {
                this.RemainingTriggers.SetValue(this.RemainingTriggers.Value - 1);
            }
            
            this.onTriggered.Invoke();
            this.OnTrigger();
        }
    }
}