using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Triggers.Conditions
{
    public abstract class ConditionTestBase : MonoBehaviour, IConditionTest
    {
        [FoldoutGroup("Settings")] [SerializeField]
        private ConditionTestType type = ConditionTestType.And;

        [FoldoutGroup("Settings")] [SerializeField]
        private BooleanReference isTestEnabled = new BooleanReference(true);

        public ConditionTestType Type => this.type;

        public bool IsTestEnabled => this.isTestEnabled.Value;
        
        public abstract bool EvaluateTest();
    }
}