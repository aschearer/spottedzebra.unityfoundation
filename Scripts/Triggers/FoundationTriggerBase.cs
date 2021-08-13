using System.Collections;
using SpottedZebra.UnityFoundation.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Triggers
{
    public abstract class FoundationTriggerBase : MonoBehaviour
    {
        [HideLabel] [Multiline] [Tooltip("For internal purposes only")]
        public string Notes;

        [Tooltip("Decremented every time the trigger fires, stops firing when zero. -1 means run forever.")]
        public IntReference RemainingTriggers = new IntReference(-1);

        [HorizontalGroup("Timer")]
        [Range(0f, 60)]
        [SerializeField]
        [Tooltip("How long to wait before the trigger evaluates once triggered")]
        private float timeToWait = 0;

        [ValueDropdown("FriendlyTimeScaleNames", AppendNextDrawer = true, DropdownWidth = 200)]
        [HideLabel]
        [HorizontalGroup("Timer", Width = 30)]
        [Tooltip("Should the timer use scaled or unscaled time?")]
        public bool isTimerTimeScaled = true;

        [HorizontalGroup("Cooldown")] [Tooltip("Time until the trigger can be fire again")] [Range(0, 60)]
        public float cooldown = 0.1f;

        [ValueDropdown("FriendlyTimeScaleNames", AppendNextDrawer = true, DropdownWidth = 200)]
        [HideLabel]
        [HorizontalGroup("Cooldown", Width = 30)]
        [Tooltip("Should the cooldown use scaled or unscaled time?")]
        public bool isCooldownTimeScaled = true;

        [PropertyOrder(1000)] [Tooltip("Should this trigger fire other triggers when it finishes?")] [SerializeField]
        private bool hasCallbacks = false;

        [PropertyOrder(1001)] [SerializeField] [ShowIf("hasCallbacks")]
        private UnityEvent onSuccess = new UnityEvent();

        [PropertyOrder(1002)] [SerializeField] [ShowIf("hasCallbacks")]
        private UnityEvent onFailure = new UnityEvent();

        private float cooledDownTime;

        private bool isFiringAsync;

#if ODIN_INSPECTOR
        private ValueDropdownList<bool> FriendlyTimeScaleNames =>
            new ValueDropdownList<bool>()
            {
                new ValueDropdownItem<bool>("Scaled Time", true),
                new ValueDropdownItem<bool>("Unscaled Time", false),
            };
#endif
        
        [PropertyOrder(2000)]
        [Button]
        public void Trigger()
        {
            float time = this.isCooldownTimeScaled ? Time.time : Time.unscaledTime;
            if ((this.RemainingTriggers.Value > 0 || this.RemainingTriggers.Value < 0) &&
                this.cooledDownTime <= time &&
                !this.isFiringAsync)
            {
                if (this.timeToWait <= 0)
                {
                    this.EvaluateTrigger();
                }
                else
                {
                    this.StartCoroutine(this.EvaluateTriggerAsync());
                }
            }
        }

        protected abstract bool OnTrigger();

        private IEnumerator EvaluateTriggerAsync()
        {
            // lock the trigger while we wait for the timer
            this.isFiringAsync = true;

            // wait for time to pass
            float timeRemaining = this.timeToWait;
            while (timeRemaining > 0)
            {
                timeRemaining -= this.isTimerTimeScaled ? Time.deltaTime : Time.unscaledDeltaTime;
                yield return null;
            }

            // evaluate the trigger, then unlock it
            this.EvaluateTrigger();
            this.isFiringAsync = false;
        }

        private void EvaluateTrigger()
        {
            float time = this.isCooldownTimeScaled ? Time.time : Time.unscaledTime;
            if (this.RemainingTriggers.Value > 0)
            {
                this.RemainingTriggers.SetValue(this.RemainingTriggers.Value - 1);
            }

            this.cooledDownTime = time + this.cooldown;
            if (this.OnTrigger())
            {
                if (this.hasCallbacks)
                {
                    this.onSuccess.Invoke();
                }
            }
            else
            {
                if (this.hasCallbacks)
                {
                    this.onFailure.Invoke();
                }
            }
        }
    }
}