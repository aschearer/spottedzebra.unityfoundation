#if EASY_SAVE
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage.EasySave
{
    [DisallowMultipleComponent]
    public class EasySaveStorage : MonoBehaviour, IStorageWriter, IStorageReader, IStorageDeleter
    {
        public void Write<T>(string id, StorageScope scope, T value)
        {
            ES3.Save(id, value, scope.ToSaveFileName());
        }

        public T Read<T>(string id, StorageScope scope, T defaultValue)
        {
            T value = ES3.Load(id, defaultValue, new ES3Settings(scope.ToSaveFileName()));
            return value;
        }

        public void Delete(StorageScope scope)
        {
            ES3.DeleteFile(scope.ToSaveFileName());
        }

        public void LoadCachesFromDisk()
        {
            StorageScope scope = this.GetScope();
            ES3.CacheFile(scope.ToSaveFileName());
        }

        public void SaveCachesToDisk()
        {
            StorageScope scope = this.GetScope();
            ES3.StoreCachedFile(scope.ToSaveFileName());
        }

        public void DeleteCachesFromDisk()
        {
            StorageScope scope = this.GetScope();
            ES3.DeleteFile(scope.ToSaveFileName()); // clear's cache
            ES3.DeleteFile(new ES3Settings(scope.ToSaveFileName()) {location = ES3.Location.File}); // deletes file
        }

        private StorageScope GetScope()
        {
            return this.GetComponent<VariableStorage>().Scope;
        }

        private void Awake()
        {
            if (ES3Settings.defaultSettings.location == ES3.Location.Cache &&
                this.TryGetComponent(out VariableStorage variableStorage))
            {
                variableStorage.onSaveFinished.AddListener(this.SaveCachesToDisk);
                variableStorage.onLoadStarted.AddListener(this.LoadCachesFromDisk);
                variableStorage.onDeleteFinished.AddListener(this.DeleteCachesFromDisk);
            }
        }

        private void OnDestroy()
        {
            if (ES3Settings.defaultSettings.location == ES3.Location.Cache && 
                this.TryGetComponent(out VariableStorage variableStorage))
            {
                variableStorage.onSaveFinished.RemoveListener(this.SaveCachesToDisk);
                variableStorage.onLoadStarted.RemoveListener(this.LoadCachesFromDisk);
                variableStorage.onDeleteFinished.RemoveListener(this.DeleteCachesFromDisk);
            }
        }
    }
}
#endif