using System.Collections;
using SpottedZebra.UnityFoundation.Variables;
using SpottedZebra.UnityFoundation.Variables.Tests;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers
{
    [RequireComponent(typeof(ConditionTrigger))]
    public class WatchConditionalTrigger : MonoBehaviour
    {
        private ConditionTrigger tests;

        private void Awake()
        {
            this.tests = this.GetComponent<ConditionTrigger>();
            foreach (BooleanTest test in this.tests.BooleanTests)
            {
                if (!test.Value.UseConstant)
                {
                    BooleanVariable variable = test.Value.GetVariable();
                    if (variable.VariableChangeEvent != null)
                    {
                        variable.VariableChangeEvent.RegisterListener(this.OnVariableChanged);
                    }
                }
            }
            
            foreach (IntTest test in this.tests.IntTests)
            {
                if (!test.Value.UseConstant)
                {
                    IntVariable variable = test.Value.GetVariable();
                    if (variable.VariableChangeEvent != null)
                    {
                        variable.VariableChangeEvent.RegisterListener(this.OnVariableChanged);
                    }
                }
            }
            
            foreach (FloatTest test in this.tests.FloatTests)
            {
                if (!test.Value.UseConstant)
                {
                    FloatVariable variable = test.Value.GetVariable();
                    if (variable.VariableChangeEvent != null)
                    {
                        variable.VariableChangeEvent.RegisterListener(this.OnVariableChanged);
                    }
                }
            }
        }

        private IEnumerator Start()
        {
            yield return null; // wait a frame for things to load up
            this.tests.Trigger();
        }

        private void OnDestroy()
        {
            if (this.tests == null)
            {
                return;
            }
            
            foreach (BooleanTest booleanTest in this.tests.BooleanTests)
            {
                if (!booleanTest.Value.UseConstant)
                {
                    BooleanVariable variable = booleanTest.Value.GetVariable();
                    if (variable.VariableChangeEvent != null)
                    {
                        variable.VariableChangeEvent.UnregisterListener(this.OnVariableChanged);
                    }
                }   
            }
            
            foreach (IntTest test in this.tests.IntTests)
            {
                if (!test.Value.UseConstant)
                {
                    IntVariable variable = test.Value.GetVariable();
                    if (variable.VariableChangeEvent != null)
                    {
                        variable.VariableChangeEvent.UnregisterListener(this.OnVariableChanged);
                    }
                }
            }
            
            foreach (FloatTest test in this.tests.FloatTests)
            {
                if (!test.Value.UseConstant)
                {
                    FloatVariable variable = test.Value.GetVariable();
                    if (variable.VariableChangeEvent != null)
                    {
                        variable.VariableChangeEvent.UnregisterListener(this.OnVariableChanged);
                    }
                }
            }
        }

        private void OnVariableChanged()
        {
            this.tests.Trigger();
        }
    }
}