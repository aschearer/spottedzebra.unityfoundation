#if EASY_SAVE
using System;
using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage.EasySave
{
    [DisallowMultipleComponent]
    public abstract class EasySaveScopeObjectBase : MonoBehaviour
    {
        [Required] [SerializeField] [InlineButton("RandomId", "New", ShowIf = "CanCreateId")]
        private StringReference id;
        
        [Required] [SerializeField] private StorageScope scope;

        private string Id => this.id.Value;

        private bool CanCreateId => this.id.UseConstant && string.IsNullOrEmpty(this.id.Value);

        [Button]
        public void Save()
        {
            this.OnSave(this.Id, this.scope.ToSaveFileName());
        }

        [Button]
        public void Load()
        {
            this.OnLoad(this.Id, this.scope.ToSaveFileName());
        }

        [Button]
        public void Delete()
        {
            this.OnDelete(this.Id, this.scope.ToSaveFileName());
        }

        protected abstract void OnSave(string id, string fileName);

        protected abstract void OnLoad(string id, string fileName);

        protected abstract void OnDelete(string id, string fileName);

        private void Awake()
        {
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

        private void RandomId()
        {
#if UNITY_EDITOR
            if (this.id.UseConstant)
            {
                Guid guid = Guid.NewGuid();
                this.id = new StringReference(guid.ToString());
                UnityEditor.EditorUtility.SetDirty(this);
            }
#endif
        }
    }
}
#endif