using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Triggers.Conditions
{
    public abstract class ObservableConditionTestBase : ConditionTestBase, IObservable
    {
        [HideInInspector] [SerializeField] private UnityEvent onChanged = new UnityEvent();

        public UnityEvent OnChanged => this.onChanged;
    }
}