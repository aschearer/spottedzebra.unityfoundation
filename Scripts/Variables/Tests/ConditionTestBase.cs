using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Triggers;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    public abstract class ConditionTestBase : MonoBehaviour, IConditionTest
    {
        [FoldoutGroup("Settings")] [SerializeField]
        private IConditionTest.TestType type = IConditionTest.TestType.And;

        [FoldoutGroup("Settings")] [SerializeField]
        private BooleanReference isTestEnabled = new BooleanReference(true);

        public IConditionTest.TestType Type => this.type;

        public bool IsTestEnabled => this.isTestEnabled.Value;
        
        public abstract bool EvaluateTest();
    }
}