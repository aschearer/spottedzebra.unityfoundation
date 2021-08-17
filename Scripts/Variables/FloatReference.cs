using System;

namespace SpottedZebra.UnityFoundation.Variables
{
    [Serializable]
    public sealed class FloatReference : VariableReferenceBase<FloatVariable, float>
    {
        public FloatReference()
            : this(0)
        {
        }
        
        public FloatReference(float value)
        {
            this.UseConstant = true;
            this.SetValue(value);
        }
    }
}