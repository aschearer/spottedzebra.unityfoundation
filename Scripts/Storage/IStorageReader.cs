namespace SpottedZebra.UnityFoundation.Storage
{
    public interface IStorageReader
    {
        T Read<T>(string id, StorageScope scope, T defaultValue);
    }
}