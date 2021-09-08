#if NODE_CANVAS
using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Tasks.Variables
{
    [Category("F/Data/Variables/Set")]
    public sealed class SetStoryBeat : ActionTask
    {
        [RequiredField] public BBParameter<StoryBeatVariable> Variable;

        [SerializeField, ExposeField] internal BBParameter<StoryBeatAccessType> Operation;
        
        public BBParameter<bool> Value;

        protected override string info => string.Format("{0}.{1}={2}", this.Variable, this.Operation, this.Value);

        protected override void OnExecute()
        {
            bool result;
            if (this.Value.isNoneOrNull || this.Variable.isNoneOrNull)
            {
                result = false;
            }
            else
            {
                switch (this.Operation.value)
                {
                    case StoryBeatAccessType.IsUnlocked:
                        this.Variable.value.SetIsUnlock(this.Value.value);
                        break;
                    case StoryBeatAccessType.IsSeen:
                        this.Variable.value.SetIsSeen(this.Value.value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                result = true;
            }

            this.EndAction(result);
        }
    }
}
#endif