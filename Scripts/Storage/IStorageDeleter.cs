namespace SpottedZebra.UnityFoundation.Storage
{
    public interface IStorageDeleter
    {
        void Delete(StorageScope scope);
    }
}