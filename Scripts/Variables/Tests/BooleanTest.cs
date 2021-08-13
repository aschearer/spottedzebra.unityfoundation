using System;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    [Serializable]
    public class BooleanTest
    {
        public BooleanReference Value;

        public BooleanTestType Operation;

        public BooleanReference Target;

        public bool EvaluateTest()
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