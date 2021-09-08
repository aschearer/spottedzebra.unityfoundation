#if NODE_CANVAS
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;
using SpottedZebra.UnityFoundation.Variables.Tests;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Tasks.Variables
{
    [Category("F/Data/Variables/Get")]
    public sealed class IsStoryBeat : ConditionTask
    {
        [RequiredField] public BBParameter<StoryBeatVariable> Variable;

        [SerializeField, ExposeField]
        internal BBParameter<StoryBeatTestType> Operation = new BBParameter<StoryBeatTestType>(StoryBeatTestType.UnlockedAndUnseen);

        protected override string info => string.Format("{0}.{1}", this.Variable, this.Operation);

        protected override bool OnCheck()
        {
            bool result = StoryBeatTest.EvaluteTestStatic(
                this.Variable.value.Value,
                this.Operation.value,
                true);
            return result;
        }
    }
}
#endif