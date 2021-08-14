using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers
{
    public class ConditionTrigger : BooleanTriggerBase
    {
        protected override bool OnTriggerBool()
        {
            bool result = this.Evaluate();
            return result;
        }

        public bool Evaluate()
        {
            IConditionTest[] tests = this.GetComponents<IConditionTest>();
            bool result = true;
            for (int i = 0; i < tests.Length; i++)
            {
                IConditionTest test = tests[i];
                if (!test.IsTestEnabled)
                {
                    continue;
                }

                bool testResult = test.EvaluateTest();
                switch (test.Type)
                {
                    case ConditionTestType.And:
                        result = testResult && result;
                        break;
                    case ConditionTestType.Or:
                        result = testResult || result;
                        break;
                    default:
                        Debug.LogErrorFormat(this, "Unexpected TestType: {0}", test.Type);
                        break;
                }
            }

            return result;
        }
    }
}