#if FLOW_CANVAS
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;

namespace SpottedZebra.UnityFoundation.Flows.Variables
{
    [Category("F/Data/Variables/Get")]
    public abstract class GetVariableBase<TVariable, TValue> : PureFunctionNode<TValue>
        where TVariable : VariableBase<TValue>
    {
        [RequiredField]
        public BBParameter<TVariable> Variable;

        public override string name => string.Format("Get {0}", this.Variable);

        public override TValue Invoke()
        {
            TValue result = this.Variable.isNoneOrNull ? default(TValue) : this.Variable.value.Value;
            return result;
        }
    }
}
#endif