using System;
using SpottedZebra.UnityFoundation.Triggers.Conditions;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    public sealed class BooleanTest : ObservableConditionTestBase
    {
        [Space]
        public BooleanReference Value = new BooleanReference() { UseConstant = false };

        public BooleanTestType Operation;

        public BooleanReference Target = new BooleanReference(true);

        public override bool EvaluateTest()
        {
            bool result;
            switch (this.Operation)
            {
                case BooleanTestType.Equals:
                    result = this.Value.Value == this.Target.Value;
                    break;
                case BooleanTestType.NotEquals:
                    result = this.Value.Value != this.Target.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public enum BooleanTestType
        {
            Equals,
            NotEquals,
        }
    }
}