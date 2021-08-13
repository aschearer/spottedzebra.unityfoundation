using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    [CreateAssetMenu(fileName = "Float Variable", menuName = "F/Variables/Float")]
    public class FloatVariable : VariableBase<float>
    {
        public void SetValueFrom(FloatVariable variable)
        {
            this.SetValue(variable.Value);
        }
    }
}