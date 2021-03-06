using System;

namespace SpottedZebra.UnityFoundation.Variables
{
    [Serializable]
    public sealed class IntReference : VariableReferenceBase<IntVariable, int>
    {
        public IntReference()
        {
        }

        public IntReference(int value)
        {
            this.UseConstant = true;
            this.SetValue(value);
        }
    }
}