#if EASY_SAVE
using System.Collections.Generic;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage.EasySave
{
    [DisallowMultipleComponent]
    public class EasySaveStorage : MonoBehaviour, IStorageWriter, IStorageReader, IStorageDeleter
    {
        public void Write<T>(string id, StorageScope scope, T value)
        {
            ES3.Save(id, value, scope.Id);
        }

        public T Read<T>(string id, StorageScope scope, T defaultValue)
        {
            T value = ES3.Load(id, defaultValue, new ES3Settings(scope.Id));
            return value;
        }

        public void Delete(StorageScope scope)
        {
            ES3.DeleteFile(scope.Id);
        }

        public void LoadCachesFromDisk()
        {
            foreach (StorageScope scope in this.GetScopes())
            {
                ES3.CacheFile(scope.Id);
            }
        }

        public void SaveCachesToDisk()
        {
            foreach (StorageScope scope in this.GetScopes())
            {
                ES3.StoreCachedFile(scope.Id);
            }
        }

        public void DeleteCachesFromDisk()
        {
            foreach (StorageScope scope in this.GetScopes())
            {
                ES3.DeleteFile(scope.Id); // clear's cache
                ES3.DeleteFile(new ES3Settings(scope.Id) { location = ES3.Location.File }); // deletes file
            }
        }

        private IEnumerable<StorageScope> GetScopes()
        {
            return this.GetComponent<VariableStorage>().Scopes;
        }
    }
}
#endif