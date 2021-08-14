using System.Collections.Generic;
using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage
{
    [CreateAssetMenu(fileName = "Variable Lookup Table", menuName = "F/Storage/Variable Lookup Table")]
    public class VariableLookupTable : ScriptableObject
    {
        public VariableBase[] Variables = null;

#if UNITY_EDITOR
        [Button]
        public void Refresh()
        {
            List<VariableBase> variables = new List<VariableBase>();
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:VariableBase");
            foreach (string guid in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                VariableBase variable = UnityEditor.AssetDatabase.LoadAssetAtPath<VariableBase>(path);
                variables.Add(variable);
            }

            this.Variables = variables.ToArray();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        protected void OnValidate()
        {
            this.Refresh();
        }
    }
}