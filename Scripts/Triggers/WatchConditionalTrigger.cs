using System.Collections;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers
{
    [RequireComponent(typeof(ConditionTrigger))]
    public class WatchConditionalTrigger : MonoBehaviour
    {
        private ConditionTrigger condition;

        private void Awake()
        {
            this.condition = this.GetComponent<ConditionTrigger>();
            IConditionTest[] tests = this.GetComponents<IConditionTest>();
            foreach (IConditionTest test in tests)
            {
                IObservable observableTest = test as IObservable;
                if (observableTest != null)
                {
                    observableTest.OnChanged.AddListener(this.OnVariableChanged);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return null; // wait a frame for things to load up
            this.condition.Trigger();
        }

        private void OnDestroy()
        {
            IConditionTest[] tests = this.GetComponents<IConditionTest>();
            foreach (IConditionTest test in tests)
            {
                IObservable observableTest = test as IObservable;
                if (observableTest != null)
                {
                    observableTest.OnChanged.AddListener(this.OnVariableChanged);
                }
            }
        }

        private void OnVariableChanged()
        {
            this.condition.Trigger();
        }
    }
}