using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    [CreateAssetMenu(fileName = "String Variable", menuName = "F/Variables/String")]
    public sealed class StringVariable : VariableBase<string>
    {
        public void SetValueFrom(StringVariable variable)
        {
            this.SetValue(variable.Value);
        }
    }
}