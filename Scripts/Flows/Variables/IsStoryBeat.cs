#if FLOW_CANVAS
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;
using SpottedZebra.UnityFoundation.Variables.Tests;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Flows.Variables
{
    [Category("F/Data/Variables/Get")]
    public sealed class IsStoryBeat : PureFunctionNode<bool>
    {
        [RequiredField] public BBParameter<StoryBeatVariable> Variable;

        [SerializeField, ExposeField] internal BBParameter<StoryBeatTestType> Operation;

        public override string name => string.Format("Is {0}.{1}", this.Variable, this.Operation);

        public override bool Invoke()
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