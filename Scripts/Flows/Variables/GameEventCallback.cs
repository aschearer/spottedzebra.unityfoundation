#if FLOW_CANVAS
using FlowCanvas;
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Flows.Variables
{
    [Category("F/Data/Variables")]
    public sealed class GameEventCallback : EventNode
    {
        [RequiredField]
        public BBParameter<GameEvent> GameEvent;

        private FlowOutput onReceived;

        public override string name
        {
            get
            {
                return base.name + string.Format(" [ <color=#DDDDDD>{0}</color> ]", this.GameEvent);
            }
        }

        public override void OnPostGraphStarted()
        {
            this.GameEvent.value.RegisterListener(this.OnGameEventRaised);
        }

        public override void OnGraphStoped()
        {
            this.GameEvent.value.UnregisterListener(this.OnGameEventRaised);
        }

        protected override void RegisterPorts()
        {
            this.onReceived = this.AddFlowOutput("Received");
        }

        private void OnGameEventRaised()
        {
            this.onReceived.Call(new Flow());
        }
    }
}
#endif