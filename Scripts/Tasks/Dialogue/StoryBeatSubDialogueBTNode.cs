#if NODE_CANVAS
using NodeCanvas.BehaviourTrees;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using SpottedZebra.UnityFoundation.Variables;
using SpottedZebra.UnityFoundation.Variables.Tests;

namespace SpottedZebra.UnityFoundation.Tasks.Dialogue
{
    [Name("StoryBeat Sub Dialogue")]
    [Category("F/Dialogue")]
    [Description("Execute the dialogue tree if the story beat is new. Mark story beat as seen afterwards.")]
    [Icon("Dialogue")]
    [DropReferenceType(typeof(DialogueTree))]
    public class StoryBeatSubDialogueBTNode : BTNodeNested<DialogueTree>
    {
        [RequiredField] public BBParameter<StoryBeatVariable> StoryBeat;

        public BBParameter<StoryBeatTest.StoryBeatTestType> Test = new BBParameter<StoryBeatTest.StoryBeatTestType>(StoryBeatTest.StoryBeatTestType.UnlockedAndUnseen);
        
        public BBParameter<CompactStatus> ResultOnStoryBeatNotNew = new BBParameter<CompactStatus>(CompactStatus.Failure);

        [SerializeField] [ExposeField] [Name("Sub Tree")]
        private BBParameter<DialogueTree> _nestedDialogueTree = null;

        private bool hasStartedSubTree;

        public override DialogueTree subGraph
        {
            get
            {
                return _nestedDialogueTree.value;
            }
            set
            {
                _nestedDialogueTree.value = value;
            }
        }

        public override BBParameter subGraphParameter => _nestedDialogueTree;

        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
            if (!this.hasStartedSubTree && 
                !StoryBeatTest.EvaluteTestStatic(this.StoryBeat.value.Value, this.Test.value, true))
            {
                return this.ResultOnStoryBeatNotNew.value == CompactStatus.Success
                    ? Status.Success
                    : Status.Failure;
            }

            if (subGraph == null || subGraph.primeNode == null)
            {
                return Status.Optional;
            }

            if (status == Status.Resting)
            {
                status = Status.Running;
                foreach (DialogueTree.ActorParameter actorParameter in this.subGraph.actorParameters)
                {
                    Variable<GameObject> speaker = this.graphBlackboard.GetVariable<GameObject>(actorParameter.name);
                    if (speaker != null)
                    {
                        this.subGraph.SetActorReference(actorParameter.name, speaker.value.GetComponent<IDialogueActor>());
                    }
                }
                
                this.hasStartedSubTree = true;
                this.TryStartSubGraph(agent, OnDLGFinished);
            }

            if (status == Status.Running)
            {
                currentInstance.UpdateGraph();
            }

            return status;
        }

        void OnDLGFinished(bool success)
        {
            if (status == Status.Running)
            {
                if (success)
                {
                    this.StoryBeat.value.SetIsSeen(true);
                }
                
                status = success ? Status.Success : Status.Failure;
            }
        }

        protected override void OnReset()
        {
            this.hasStartedSubTree = false;
            if (currentInstance != null)
            {
                currentInstance.Stop();
            }
        }
    }
}
#endif