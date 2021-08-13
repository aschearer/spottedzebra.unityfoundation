using System.Collections.Generic;
using SpottedZebra.UnityFoundation.Variables.Tests;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers
{
    public class ConditionTrigger : FoundationTriggerBase
    {
        [SerializeField] [Tooltip("How are the different conditions combined?")] [ValueDropdown("FriendlyTestNames")]
        private bool testType = true;

        [SerializeField] private List<BooleanTest> booleanTests = new List<BooleanTest>();
        
        [SerializeField] private List<IntTest> intTests = new List<IntTest>();
        
        [SerializeField] private List<FloatTest> floatTests = new List<FloatTest>();

        public List<BooleanTest> BooleanTests => this.booleanTests;

        public List<IntTest> IntTests => this.intTests;

        public List<FloatTest> FloatTests => this.floatTests;
        
        protected bool TestType => this.testType;

#if ODIN_INSPECTOR
        private ValueDropdownList<bool> FriendlyTestNames => new ValueDropdownList<bool>()
        {
            { "All tests must be pass", true },
            { "Any test can pass", false },
        };
#endif

        protected override bool OnTrigger()
        {
            bool result = this.Evaluate();
            return result;
        }

        public virtual bool Evaluate()
        {
            if (this.booleanTests.Count == 0 && 
                this.intTests.Count == 0)
            {
                return true;
            }

            bool result = false;
            bool isAnyTrue = false;
            bool areAllTrue = true;
            if (this.booleanTests.Count > 0)
            {
                foreach (BooleanTest booleanTest in this.booleanTests)
                {
                    bool testResult = booleanTest.EvaluateTest();
                    isAnyTrue = isAnyTrue || testResult;
                    areAllTrue = areAllTrue && testResult;

                    if (this.testType && !areAllTrue)
                    {
                        break;
                    }
                }

                result = this.testType ? areAllTrue : isAnyTrue;
            }

            if (this.intTests.Count > 0)
            {
                foreach (IntTest intTest in this.intTests)
                {
                    bool testResult = intTest.EvaluateTest();
                    isAnyTrue = isAnyTrue || testResult;
                    areAllTrue = areAllTrue && testResult;

                    if (this.testType && !areAllTrue)
                    {
                        break;
                    }
                }

                result = this.testType ? areAllTrue : isAnyTrue;
            }

            if (this.floatTests.Count > 0)
            {
                foreach (FloatTest floatTest in this.floatTests)
                {
                    bool testResult = floatTest.EvaluateTest();
                    isAnyTrue = isAnyTrue || testResult;
                    areAllTrue = areAllTrue && testResult;

                    if (this.testType && !areAllTrue)
                    {
                        break;
                    }
                }

                result = this.testType ? areAllTrue : isAnyTrue;
            }

            return result;
        }
    }
}