#if NODE_CANVAS
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Tasks.Variables
{
    [Category("F/Data/Variables/Set")]
    public abstract class SetVariableBase<TVariable, TValue> : ActionTask
        where TVariable : VariableBase<TValue>
    {
        [RequiredField]
        public BBParameter<TVariable> Variable;
        
        public BBParameter<TValue> Value;

        protected override string info => string.Format("{0}={1}", this.Variable, this.Value);

        protected override void OnExecute()
        {
            bool result;
            if (this.Value.isNoneOrNull || this.Variable.isNoneOrNull)
            {
                result = false;
            }
            else
            {
                this.Variable.value.SetValue(this.Value.value);
                result = true;
            }

            this.EndAction(result);
        }
    }
}
#endif