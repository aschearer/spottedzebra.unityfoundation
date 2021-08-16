#if EASY_SAVE
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage.EasySave
{
    public sealed class EasySaveScopedCollection : EasySaveScopeObjectBase
    {
        protected override void OnSave(string id, string fileName)
        {
            GameObject[] children = new GameObject[this.transform.childCount];
            for (int i = 0; i < this.transform.childCount; i++)
            {
                children[i] = this.transform.GetChild(i).gameObject;
            }
            
            ES3.Save(id, children, fileName);
        }

        protected override void OnLoad(string id, string fileName)
        {
            GameObject[] items = ES3.Load<GameObject[]>(id, new ES3Settings(fileName));
            foreach (GameObject item in items)
            {
                item.transform.SetParent(this.transform);
            }
        }

        protected override void OnDelete(string id, string fileName)
        {
            ES3.DeleteKey(id, fileName);
        }
    }
}
#endif