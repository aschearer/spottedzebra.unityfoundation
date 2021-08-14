using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Storage;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    public abstract class VariableBase<T> : VariableBase
    {
        [Tooltip("Starting value for the variable")]
        public T StartingValue;

        [ShowInInspector]
        public T Value { get; private set; }

        [Button]
        public void SetValue(T value)
        {
            Debug.Assert(!this.IsConstant, this);
            this.SetValueSilently(value);
            if (this.VariableChangeEvent != null)
            {
                this.VariableChangeEvent.RaiseEvent();
            }
        }

        public void SetValueSilently(T value)
        {
            this.Value = value;
        }

        public override void ResetToStartingValue(bool raiseChangeEvent = true)
        {
            if (raiseChangeEvent)
            {
                this.SetValue(this.StartingValue);
            }
            else
            {
                this.SetValueSilently(this.StartingValue);
            }
        }

        public override void Save(IStorageWriter writer)
        {
            writer.Write(this.Id, this.storageScope, this.Value);
        }

        public override void Load(IStorageReader reader)
        {
            T value = reader.Read(this.Id, this.storageScope, this.StartingValue);
            this.SetValueSilently(value);
        }
    }
}