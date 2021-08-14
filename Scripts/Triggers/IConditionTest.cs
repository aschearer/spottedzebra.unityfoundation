namespace SpottedZebra.UnityFoundation.Triggers
{
    public interface IConditionTest
    {
        ConditionTestType Type { get; }
        
        bool IsTestEnabled { get; }
        
        bool EvaluateTest();
    }
}