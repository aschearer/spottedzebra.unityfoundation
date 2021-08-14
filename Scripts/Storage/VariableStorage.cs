using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Storage
{
    [ExecuteAlways]
    public class VariableStorage : MonoBehaviour
    {
        [SerializeField] private StorageScope[] scopes = null;

        [InlineButton("CreateLookupTable", "New", ShowIf = "ShowCreateLookupTable")]
        [Required] [SerializeField] private VariableLookupTable variableLookupTable;

        [Space] [PropertyOrder(2000)] [FoldoutGroup("Save")] [SerializeField]
        protected UnityEvent onSaveStarted = new UnityEvent();

        [PropertyOrder(2001)] [FoldoutGroup("Save")] [SerializeField] private UnityEvent onSaveFinished = new UnityEvent();

        [PropertyOrder(2000)] [FoldoutGroup("Load")] [SerializeField]
        protected UnityEvent onLoadStarted = new UnityEvent();

        [PropertyOrder(2001)] [FoldoutGroup("Load")] [SerializeField] private UnityEvent onLoadFinished = new UnityEvent();

        [PropertyOrder(2000)] [FoldoutGroup("Delete")] [SerializeField]
        protected UnityEvent onDeleteStarted = new UnityEvent();

        [PropertyOrder(2001)] [FoldoutGroup("Delete")] [SerializeField] private UnityEvent onDeleteFinished = new UnityEvent();

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
                else
                {
                    shouldPersist = false;
                    foreach (StorageScope persistenceType in this.scopes)
                    {
                        if (variable.storageScope == persistenceType)
                        {
                            shouldPersist = true;
                            break;
                        }
                    }
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
                else
                {
                    shouldPersist = false;
                    foreach (StorageScope persistenceType in this.scopes)
                    {
                        if (variable.storageScope == persistenceType)
                        {
                            shouldPersist = true;
                            break;
                        }
                    }
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
            foreach (StorageScope scope in this.scopes)
            {
                foreach (IStorageDeleter deleter in deleters)
                {
                    deleter.Delete(scope);
                }
            }
            
            this.onDeleteFinished.Invoke();
        }

#if UNITY_EDITOR
        private void Awake()
        {
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
        }

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