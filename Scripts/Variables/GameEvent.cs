using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "F/Variables/Game Event")]
    public class GameEvent : ScriptableObject
    {
        public delegate void GameEventListener();

        [ShowInInspector]
        [DisplayAsString]
        public string Guid
        {
            get
            {
                string guid = string.Empty;
#if UNITY_EDITOR
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                guid = UnityEditor.AssetDatabase.AssetPathToGUID(assetPath);
#endif
                return guid;
            }
        }

        private List<GameEventListener> listeners = new List<GameEventListener>();

        [Button]
        public void RaiseEvent()
        {
            for (int i = this.listeners.Count - 1; i >= 0; i--)
            {
                this.listeners[i]();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            this.listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            this.listeners.Remove(listener);
        }
    }
}