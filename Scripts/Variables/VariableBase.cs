using System;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    public abstract class VariableBase : ScriptableObject, IEquatable<VariableBase>
    {
        public abstract string Id { get; }
        
        public abstract void ResetToStartingValue();

        public bool Equals(VariableBase other)
        {
            return other != null && string.CompareOrdinal(this.Id, other.Id) == 0;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as VariableBase);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}