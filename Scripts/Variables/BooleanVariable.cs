using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    [CreateAssetMenu(fileName = "Boolean Variable", menuName = "F/Variables/Boolean")]
    public sealed class BooleanVariable : VariableBase<bool>
    {
        public void SetValueFrom(BooleanVariable variable)
        {
            this.SetValue(variable.Value);
        }
    }
}