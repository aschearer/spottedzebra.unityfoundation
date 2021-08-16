#if NODE_CANVAS
using NodeCanvas.Framework;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Tasks.Variables
{
    public sealed class RaiseGameEvent : ActionTask
    {
        public BBParameter<GameEvent> GameEvent;

        protected override void OnExecute()
        {
            bool result;
            if (this.GameEvent.isNoneOrNull)
            {
                result = false;
            }
            else
            {
                this.GameEvent.value.RaiseEvent();
                result = true;
            }

            this.EndAction(result);
        }
    }
}
#endif