using UnityEngine;
using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    public abstract class ObservableConditionTestBase : ConditionTestBase, IObservable
    {
        [HideInInspector] [SerializeField] private UnityEvent onChanged = new UnityEvent();

        public UnityEvent OnChanged => this.onChanged;
    }
}