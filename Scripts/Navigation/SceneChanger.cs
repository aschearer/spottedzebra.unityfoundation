using System.Collections;
using Sirenix.OdinInspector;
using SpottedZebra.UnityFoundation.Navigation.Design;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpottedZebra.UnityFoundation.Navigation
{
    [DisallowMultipleComponent]
    public class SceneChanger : MonoBehaviour
    {
        [InfoBox("Must be a root GameObject as it relies on `DontDestroyOnLoad`.")]
        [SceneRequiredAttribute] [Tooltip("Target scene to load")] [SerializeField]
        private SceneReference scene;

        [Tooltip("Called when the transition starts")] [FoldoutGroup("Events")] [SerializeField]
        private UnityEvent onStart;

        [Tooltip("Called when the new scene is loaded but obscured")] [FoldoutGroup("Events")] [SerializeField]
        private UnityEvent onSceneLoaded;

        [Tooltip("Called when the new scene is loaded and visible")] [FoldoutGroup("Events")] [SerializeField]
        private UnityEvent onComplete;

        [Button]
        public void ChangeScenes()
        {
            this.StartCoroutine(this.ChangeScenesInternal());
        }

        private IEnumerator ChangeScenesInternal()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);

            this.onStart.Invoke();

            yield return null;

            SceneChangeTransitionEffectPlayer screenEffect = null;
            if (this.TryGetComponent(out screenEffect))
            {
                yield return screenEffect.TransitionOut();
            }

            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return SceneManager.LoadSceneAsync(this.scene.ScenePath);

            this.onSceneLoaded.Invoke();

            if (screenEffect != null)
            {
                yield return screenEffect.TransitionIn();
            }

            yield return null;

            this.onComplete.Invoke();

            yield return null;
            
            GameObject.Destroy(this.gameObject);
        }

        private void Awake()
        {
            Debug.Assert(this.transform.parent == null, "Must be root GameObject", this);
        }
    }
}