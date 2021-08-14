namespace SpottedZebra.UnityFoundation.Storage
{
    public interface IStorageWriter
    {
        void Write<T>(string id, StorageScope scope, T value);
    }
}