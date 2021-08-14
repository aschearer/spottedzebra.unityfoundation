#if NODE_CANVAS
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Tasks.Variables
{
    [Category("F/Data/Variables/Get")]
    public abstract class GetVariableBase<TVarible, TValue> : ActionTask
        where TVarible : VariableBase<TValue>
    {
        public BBParameter<TValue> Value;
        
        public BBParameter<TVarible> Variable;

        protected override string info => string.Format("{0}={1}", this.Value, this.Variable);

        protected override void OnExecute()
        {
            bool result;
            if (this.Value.isNoneOrNull || this.Variable.isNoneOrNull)
            {
                result = false;
            }
            else
            {
                this.Value.SetValue(this.Variable.value.Value);
                result = true;
            }

            this.EndAction(result);
        }
    }
}
#endif