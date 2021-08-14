using System;
using SpottedZebra.UnityFoundation.Triggers.Conditions;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    public sealed class FloatTest : ObservableConditionTestBase
    {
        [Space]
        public FloatReference Value = new FloatReference() { UseConstant = false };

        public FloatTestType Operation;

        public FloatReference Target;

        public override bool EvaluateTest()
        {
            bool result;
            switch (this.Operation)
            {
                case FloatTestType.Equals:
                    result = Mathf.Approximately(this.Value.Value - this.Target.Value, 0.01f);
                    break;
                case FloatTestType.NotEquals:
                    result = Mathf.Approximately(this.Value.Value - this.Target.Value, 0.01f);
                    break;
                case FloatTestType.GreaterThan:
                    result = this.Value.Value > this.Target.Value;
                    break;
                case FloatTestType.LessThan:
                    result = this.Value.Value < this.Target.Value;
                    break;
                case FloatTestType.GreaterThanEqualToNow:
                    result = this.Value.Value >= Time.time;
                    break;
                case FloatTestType.LessThanEqualToNow:
                    result = this.Value.Value <= Time.time;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public enum FloatTestType
        {
            Equals,
            NotEquals,
            GreaterThan,
            LessThan,
            GreaterThanEqualToNow,
            LessThanEqualToNow,
        }
    }
}