using Sirenix.OdinInspector;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage
{
    [CreateAssetMenu(fileName = "Storage Scope", menuName = "F/Storage/Scope")]
    public class StorageScope : ScriptableObject
    {
        [DisplayAsString] [InlineButton("CopyId", "Copy")] [SerializeField]
        private string id;
        
        [HideLabel]
        [Multiline]
        [Tooltip("For internal purposes only")]
        public string Notes;

        public string Id => this.id;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                string guid = UnityEditor.AssetDatabase.GUIDFromAssetPath(assetPath).ToString();
                if (!guid.Equals(this.Id))
                {
                    this.id = guid;
                    UnityEditor.EditorUtility.SetDirty(this);
                }
            }
#endif
        }

        private void CopyId()
        {
#if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.systemCopyBuffer = this.id;
#endif
        }
    }
}