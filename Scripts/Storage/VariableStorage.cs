using System;
using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Storage
{
    [ExecuteAlways]
    public class VariableStorage : MonoBehaviour
    {
        [Required] [SerializeField] private StorageScope scope = null;

        [InlineButton("CreateLookupTable", "New", ShowIf = "ShowCreateLookupTable")]
        [Required] [SerializeField] private VariableLookupTable variableLookupTable;

        [Space] [PropertyOrder(2000)] [FoldoutGroup("Save")] [SerializeField]
        public UnityEvent onSaveStarted = new UnityEvent();

        [PropertyOrder(2001)] [FoldoutGroup("Save")] [SerializeField]
        public UnityEvent onSaveFinished = new UnityEvent();

        [PropertyOrder(2000)] [FoldoutGroup("Load")] [SerializeField]
        public UnityEvent onLoadStarted = new UnityEvent();

        [PropertyOrder(2001)] [FoldoutGroup("Load")] [SerializeField]
        public UnityEvent onLoadFinished = new UnityEvent();

        [PropertyOrder(2000)] [FoldoutGroup("Delete")] [SerializeField]
        public UnityEvent onDeleteStarted = new UnityEvent();

        [PropertyOrder(2001)] [FoldoutGroup("Delete")] [SerializeField]
        public UnityEvent onDeleteFinished = new UnityEvent();

        public StorageScope Scope => this.scope;

        private bool ShowCreateLookupTable => this.variableLookupTable == null;

        [Button]
        public void Save()
        {
            this.onSaveStarted.Invoke();

            IStorageWriter[] writers = this.GetComponents<IStorageWriter>();

            foreach (VariableBase variable in this.variableLookupTable.Variables)
            {
                bool shouldPersist;
                if (variable.IsConstant || variable.storageScope == null)
                {
                    shouldPersist = false;
                }
                else if (variable.storageScope == this.scope)
                {
                    shouldPersist = true;
                }
                else
                {
                    shouldPersist = false;
                }

                if (shouldPersist)
                {
                    foreach (IStorageWriter writer in writers)
                    {
                        variable.Save(writer);
                    }
                }
            }

            this.onSaveFinished.Invoke();
        }

        [Button]
        public void Load()
        {
            this.onLoadStarted.Invoke();

            IStorageReader reader = this.GetComponent<IStorageReader>();

            foreach (VariableBase variable in this.variableLookupTable.Variables)
            {
                bool shouldPersist;
                if (variable.IsConstant || variable.storageScope == null)
                {
                    shouldPersist = false;
                }
                else if (variable.storageScope == scope)
                {
                    shouldPersist = true;
                }
                else
                {
                    shouldPersist = false;
                }

                if (shouldPersist)
                {
                    variable.Load(reader);
                }
                else
                {
                    variable.ResetToStartingValue(false);
                }
            }
            
            this.onLoadFinished.Invoke();
        }

        [Button]
        public void Delete()
        {
            this.onDeleteStarted.Invoke();
            IStorageDeleter[] deleters = this.GetComponents<IStorageDeleter>();
            foreach (IStorageDeleter deleter in deleters)
            {
                deleter.Delete(scope);
            }
            
            this.onDeleteFinished.Invoke();
        }

        private void Awake()
        {
#if UNITY_EDITOR
            if (this.variableLookupTable == null)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:VariableLookupTable");
                foreach (string guid in guids)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                    VariableLookupTable lookupTable = UnityEditor.AssetDatabase.LoadAssetAtPath<VariableLookupTable>(path);
                    this.variableLookupTable = lookupTable;
                    UnityEditor.EditorUtility.SetDirty(this);
                    break;
                }
            }
#endif
            this.scope.SaveScope.RegisterListener(this.Save);
            this.scope.LoadScope.RegisterListener(this.Load);
            this.scope.DeleteScope.RegisterListener(this.Delete);
        }

        private void OnDestroy()
        {
            this.scope.SaveScope.UnregisterListener(this.Save);
            this.scope.LoadScope.UnregisterListener(this.Load);
            this.scope.DeleteScope.UnregisterListener(this.Delete);
        }

#if UNITY_EDITOR
            private void CreateLookupTable()
        {
            VariableLookupTable lookupTable = ScriptableObject.CreateInstance<VariableLookupTable>();
            lookupTable.name = "Variable Lookup Table";

            string assetPath = string.Format("{0}/{1}.asset", "Assets", lookupTable.name);
            UnityEditor.AssetDatabase.CreateAsset(lookupTable, assetPath);

            this.variableLookupTable = lookupTable;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}