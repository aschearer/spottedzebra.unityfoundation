using System;
using SpottedZebra.UnityFoundation.Triggers.Conditions;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    public sealed class IntTest : ObservableConditionTestBase
    {
        [Space]
        public IntReference Value = new IntReference() { UseConstant = false };

        public IntTestType Operation;

        public IntReference Target;

        public override bool EvaluateTest()
        {
            bool result;
            switch (this.Operation)
            {
                case IntTestType.Equals:
                    result = this.Value.Value == this.Target.Value;
                    break;
                case IntTestType.NotEquals:
                    result = this.Value.Value != this.Target.Value;
                    break;
                case IntTestType.GreaterThan:
                    result = this.Value.Value > this.Target.Value;
                    break;
                case IntTestType.LessThan:
                    result = this.Value.Value < this.Target.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public enum IntTestType
        {
            Equals,
            NotEquals,
            GreaterThan,
            LessThan,
        }
    }
}