using System;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    [Serializable]
    public class IntTest
    {
        public IntReference Value;

        public IntTestType Operation;

        public IntReference Target;

        public bool EvaluateTest()
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