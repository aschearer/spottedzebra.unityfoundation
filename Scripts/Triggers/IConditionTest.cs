namespace SpottedZebra.UnityFoundation.Triggers
{
    public interface IConditionTest
    {
        public TestType Type { get; }
        
        public bool IsTestEnabled { get; }
        
        bool EvaluateTest();

        public enum TestType
        {
            And,
            Or,
        }
    }
}