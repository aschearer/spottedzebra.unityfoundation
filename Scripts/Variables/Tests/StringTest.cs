using System;
using SpottedZebra.UnityFoundation.Triggers.Conditions;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    public sealed class StringTest : ObservableConditionTestBase
    {
        [Space]
        public StringReference Value = new StringReference() { UseConstant = false };

        public StringTestType Operation;

        public StringReference Target;

        public override bool EvaluateTest()
        {
            bool result;
            int comparison = string.CompareOrdinal(this.Value.Value, this.Target.Value);
            switch (this.Operation)
            {
                case StringTestType.Equals:
                    result = comparison == 0;
                    break;
                case StringTestType.NotEquals:
                    result = comparison != 0;
                    break;
                case StringTestType.GreaterThan:
                    result = comparison > 0;
                    break;
                case StringTestType.LessThan:
                    result = comparison < 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public enum StringTestType
        {
            Equals,
            NotEquals,
            GreaterThan,
            LessThan,
        }
    }
}