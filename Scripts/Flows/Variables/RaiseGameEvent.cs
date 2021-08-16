#if FLOW_CANVAS
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Flows.Variables
{
    [Category("F/Data/Variables")]
    public sealed class RaiseGameEvent : CallableActionNode
    {
        [RequiredField]
        public BBParameter<GameEvent> GameEvent;

        public override string name => string.Format("Raise {0}", this.GameEvent);

        public override void Invoke()
        {
            this.GameEvent.value.RaiseEvent();
        }
    }
}
#endif