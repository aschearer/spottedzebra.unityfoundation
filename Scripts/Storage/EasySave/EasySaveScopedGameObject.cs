#if EASY_SAVE
namespace SpottedZebra.UnityFoundation.Storage.EasySave
{
    public class EasySaveScopedGameObject : EasySaveScopeObjectBase
    {
        protected override void OnSave(string id, string fileName)
        {
            ES3.Save(id, this.gameObject, fileName);
        }

        protected override void OnLoad(string id, string fileName)
        {
            ES3.LoadInto(id, this.gameObject, new ES3Settings(fileName));
        }

        protected override void OnDelete(string id, string fileName)
        {
            ES3.DeleteKey(id, fileName);
        }
    }
}
#endif