using System;

namespace SpottedZebra.UnityFoundation.Variables
{
    [Serializable]
    public class BooleanReference : VariableReferenceBase<BooleanVariable, bool>
    {
        public BooleanReference()
            : this(false)
        {
        }

        public BooleanReference(bool value)
        {
            this.UseConstant = true;
            this.SetValue(value);
        }
    }
}