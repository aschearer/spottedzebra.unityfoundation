using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    public abstract class VariableBase<T> : VariableBase
    {
        [DisplayAsString] [InlineButton("CopyId", "Copy")] [SerializeField]
        private string id;
        
        [Tooltip("Set to true if the value should not change at runtime")]
        public bool IsConstant;

        [Tooltip("When should the variable revert to its starting value?")] [HideIf("IsConstant")]
        public VariablePersistence Persistence;

        [LabelText("On Variable Changed")]
        [Tooltip("If set, event will be raised whenever the variable changes. (optional)")]
        public GameEvent VariableChangeEvent;

        [Tooltip("Starting value for the variable")]
        public T StartingValue;

        public override string Id => this.id;

        [ShowInInspector]
        public T Value { get; private set; }

        [Button]
        public void SetValue(T value)
        {
            Debug.Assert(!this.IsConstant, this);
            this.SetValueSilently(value);
            if (this.VariableChangeEvent != null)
            {
                this.VariableChangeEvent.RaiseEvent();
            }
        }

        public void SetValueSilently(T value)
        {
            this.Value = value;
        }

        public override void ResetToStartingValue()
        {
            this.SetValue(this.StartingValue);
        }

        [HideIf("VariableChangeEvent")]
        [Button]
        private void CreateGameEvent()
        {
#if UNITY_EDITOR
            string path = UnityEditor.AssetDatabase.GetAssetPath(this);
            string dir = Path.GetDirectoryName(path);
            GameEvent changeEvent = ScriptableObject.CreateInstance<GameEvent>();
            changeEvent.name = this.name + " Changed";

            string assetPath = string.Format("{0}/{1}.asset", dir, changeEvent.name);
            UnityEditor.AssetDatabase.CreateAsset(changeEvent, assetPath);

            this.VariableChangeEvent = changeEvent;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                string guid = UnityEditor.AssetDatabase.GUIDFromAssetPath(assetPath).ToString();
                if (!guid.Equals(this.id))
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