using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Storage;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    public abstract class VariableBase : ScriptableObject, IEquatable<VariableBase>
    {
        [DisplayAsString] [InlineButton("CopyId", "Copy")] [SerializeField]
        private string id;
        
        [Tooltip("Set to true if the value should not change at runtime")]
        public bool IsConstant;

        [Tooltip("When should the variable revert to its starting value?")] [HideIf("IsConstant")]
        public StorageScope storageScope;

        [LabelText("On Variable Changed")]
        [Tooltip("If set, event will be raised whenever the variable changes. (optional)")]
        [HideIf("IsConstant")]
        public GameEvent VariableChangeEvent;

        public string Id => this.id;
        
        public abstract void ResetToStartingValue(bool raiseChangeEvent = true);

        public abstract void Save(IStorageWriter writer);

        public abstract void Load(IStorageReader reader);

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

        [HideIf("VariableChangeEvent")]
        [Button]
        private void CreateGameEvent()
        {
#if UNITY_EDITOR
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            string dir = Path.GetDirectoryName(path);
            GameEvent changeEvent = ScriptableObject.CreateInstance<GameEvent>();
            changeEvent.name = this.name + "_Changed";

            string assetPath = string.Format("{0}/{1}.asset", dir, changeEvent.name);
            UnityEditor.AssetDatabase.CreateAsset(changeEvent, assetPath);

            this.VariableChangeEvent = changeEvent;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
            if (string.IsNullOrEmpty(assetPath))
            {
                return;
            }
            
            string assetGuid = UnityEditor.AssetDatabase.GUIDFromAssetPath(assetPath).ToString();

            if (string.IsNullOrEmpty(this.id) || !this.id.Equals(assetGuid))
            {
                // assign guid
                this.id = assetGuid;

                // register with lookup table
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:VariableLookupTable");
                foreach (string guid in guids)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                    VariableLookupTable lookupTable = UnityEditor.AssetDatabase.LoadAssetAtPath<VariableLookupTable>(path);
                    if (!lookupTable.Variables.Contains(this))
                    {
                        List<VariableBase> variables = new List<VariableBase>(lookupTable.Variables);
                        variables.Add(this);
                        lookupTable.Variables = variables.ToArray();
                        UnityEditor.EditorUtility.SetDirty(lookupTable);
                    }
                }

                UnityEditor.EditorUtility.SetDirty(this);
            }
#endif
        }

        private void Reset()
        {
            this.OnValidate();
        }

        private void CopyId()
        {
#if UNITY_EDITOR
            this.OnValidate();
            UnityEditor.EditorGUIUtility.systemCopyBuffer = this.id;
#endif
        }
    }
}