#if EASY_SAVE
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage.EasySave
{
    [DisallowMultipleComponent]
    public class EasySaveStorage : MonoBehaviour, IStorageWriter, IStorageReader, IStorageDeleter
    {
        [SerializeField] private IntReference currentSaveFileIndex;

        public void Write<T>(string id, StorageScope scope, T value)
        {
            ES3.Save(id, value, this.ToSaveFile(scope));
        }

        public T Read<T>(string id, StorageScope scope, T defaultValue)
        {
            T value = ES3.Load(id, defaultValue, new ES3Settings(this.ToSaveFile(scope)));
            return value;
        }

        public void Delete(StorageScope scope)
        {
            ES3.DeleteFile(this.ToSaveFile(scope));
        }

        public void LoadCachesFromDisk()
        {
            StorageScope scope = this.GetScope();
            ES3.CacheFile(this.ToSaveFile(scope));
        }

        public void SaveCachesToDisk()
        {
            StorageScope scope = this.GetScope();
            ES3.StoreCachedFile(this.ToSaveFile(scope));
        }

        public void DeleteCachesFromDisk()
        {
            StorageScope scope = this.GetScope();
            ES3.DeleteFile(this.ToSaveFile(scope)); // clear's cache
            ES3.DeleteFile(new ES3Settings(this.ToSaveFile(scope)) {location = ES3.Location.File}); // deletes file
        }

        private StorageScope GetScope()
        {
            return this.GetComponent<VariableStorage>().Scope;
        }

        private string ToSaveFile(StorageScope scope)
        {
            string result = string.Format("{0}_{1}.sav", this.currentSaveFileIndex.Value, scope.Id);
            return result;
        }

        private void Awake()
        {
            if (this.TryGetComponent(out VariableStorage variableStorage))
            {
                variableStorage.onSaveFinished.AddListener(this.SaveCachesToDisk);
                variableStorage.onLoadStarted.AddListener(this.LoadCachesFromDisk);
                variableStorage.onDeleteFinished.AddListener(this.DeleteCachesFromDisk);
            }
        }

        private void OnDestroy()
        {
            if (this.TryGetComponent(out VariableStorage variableStorage))
            {
                variableStorage.onSaveFinished.AddListener(this.SaveCachesToDisk);
                variableStorage.onLoadStarted.AddListener(this.LoadCachesFromDisk);
                variableStorage.onDeleteFinished.AddListener(this.DeleteCachesFromDisk);
            }
        }
    }
}
#endif