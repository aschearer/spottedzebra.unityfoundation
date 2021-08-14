#if EASY_SAVE
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
            T value = ES3.Load(id, defaultValue, new ES3Settings(scope.name));
            return value;
        }

        public void Delete(StorageScope scope)
        {
            ES3.DeleteFile(scope.Id);
        }
    }
}
#endif