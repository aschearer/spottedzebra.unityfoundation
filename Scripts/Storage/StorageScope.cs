using System.IO;
using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Storage
{
    [CreateAssetMenu(fileName = "Storage Scope", menuName = "F/Storage/Scope")]
    public class StorageScope : ScriptableObject
    {
        [DisplayAsString] [InlineButton("CopyId", "Copy")] [SerializeField]
        private string id;

        [HideLabel] [Multiline] [Tooltip("For internal purposes only")]
        public string Notes;
        
        [SerializeField] private IntReference currentSaveFileIndex;

        [Required]
        public GameEvent SaveScope;

        [Required]
        public GameEvent LoadScope;
        
        [Required]
        public GameEvent DeleteScope;

        public string Id => this.id;

        private bool CanAddGameEvents => this.SaveScope == null ||
                                         this.LoadScope == null ||
                                         this.DeleteScope == null;

        public string ToSaveFileName()
        {
            string result = string.Format("{0}_{1}.sav", this.currentSaveFileIndex.Value, this.Id);
            return result;
        }

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

#if UNITY_EDITOR
        private void CopyId()
        {
            UnityEditor.EditorGUIUtility.systemCopyBuffer = this.id;
        }

        [ShowIf("CanAddGameEvents")]
        [Button]
        private void CreateSaveGameEvent()
        {
            if (this.SaveScope == null)
            {
                this.SaveScope = this.CreateGameEvent("Save");
                UnityEditor.EditorUtility.SetDirty(this);
            }
            
            if (this.LoadScope == null)
            {
                this.LoadScope = this.CreateGameEvent("Load");
                UnityEditor.EditorUtility.SetDirty(this);
            }
            
            if (this.DeleteScope == null)
            {
                this.DeleteScope = this.CreateGameEvent("Delete");
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
        
        private GameEvent CreateGameEvent(string suffix)
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            string dir = Path.GetDirectoryName(path);
            GameEvent result = ScriptableObject.CreateInstance<GameEvent>();
            result.name = string.Format("{0}_{1}", this.name, suffix);

            string assetPath = string.Format("{0}/{1}.asset", dir, result.name);
            UnityEditor.AssetDatabase.CreateAsset(result, assetPath);

            return result;
        }
#endif
    }
}