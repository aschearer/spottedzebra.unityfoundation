namespace SpottedZebra.UnityFoundation.Triggers.Conditions
{
    public interface IConditionTest
    {
        ConditionTestType Type { get; }
        
        bool IsTestEnabled { get; }
        
        bool EvaluateTest();
    }
}