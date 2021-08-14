using System;

namespace SpottedZebra.UnityFoundation.Variables
{
    [Serializable]
    public sealed class StringReference : VariableReferenceBase<StringVariable, string>
    {
        public StringReference()
            : this(string.Empty)
        {
        }

        public StringReference(string value)
        {
            this.UseConstant = true;
            this.SetValue(value);
        }
    }
}