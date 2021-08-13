using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    [CreateAssetMenu(fileName = "Int Variable", menuName = "F/Variables/Int")]
    public class IntVariable : VariableBase<int>
    {
        public void SetValueFrom(IntVariable variable)
        {
            this.SetValue(variable.Value);
        }
    }
}