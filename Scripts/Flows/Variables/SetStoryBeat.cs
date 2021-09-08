#if FLOW_CANVAS
using System;
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Flows.Variables
{
    [Category("F/Data/Variables/Set")]
    public sealed class SetStoryBeat : CallableActionNode<bool>
    {
        [RequiredField] public BBParameter<StoryBeatVariable> Variable;

        [SerializeField, ExposeField] internal BBParameter<StoryBeatAccessType> Operation;

        public override string name => string.Format("Set {0}.{1}", this.Variable, this.Operation);

        public override void Invoke(bool value)
        {
            switch (this.Operation.value)
            {
                case StoryBeatAccessType.IsUnlocked:
                    this.Variable.value.SetIsUnlock(value);
                    break;
                case StoryBeatAccessType.IsSeen:
                    this.Variable.value.SetIsSeen(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
#endif