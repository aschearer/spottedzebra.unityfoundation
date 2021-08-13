using System;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    [Serializable]
    public abstract class VariableReferenceBase<TVariable, TValue>
        where TVariable : VariableBase<TValue>
    {
        [SerializeField] public bool UseConstant;

        [SerializeField] private TVariable Variable = default(TVariable);

        [SerializeField] private TValue ConstantValue;

        protected VariableReferenceBase(TValue value)
        {
            this.UseConstant = true;
            this.SetValue(value);
        }


        protected VariableReferenceBase()
        {
            this.UseConstant = true;
        }

        public TValue Value
        {
            get
            {
                if (!this.UseConstant && this.Variable == null)
                {
                    return default(TValue);
                }

                return this.UseConstant ? this.ConstantValue : this.Variable.Value;
            }
        }

        public TVariable GetVariable()
        {
            return this.Variable;
        }

        public void SetValue(TValue value)
        {
            if (this.UseConstant)
            {
                this.ConstantValue = value;
            }
            else
            {
                this.Variable.SetValue(value);
            }
        }
    }
}