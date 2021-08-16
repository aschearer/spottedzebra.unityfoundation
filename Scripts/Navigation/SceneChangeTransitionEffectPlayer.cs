using System.Collections;
using SpottedZebra.UnityFoundation.Polish;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Navigation
{
    [DisallowMultipleComponent]
    public sealed class SceneChangeTransitionEffectPlayer : MonoBehaviour
    {
        [Tooltip("The transition from 0=>1")]
        [SerializeField]
        private AnimationCurve percentComplete = AnimationCurve.Linear(0, 0, 1, 1);

        public IEnumerator TransitionOut()
        {
            if (this.TryGetComponent(out EffectDefinitionBase effect))
            {
                effect.Play();

                float runTime = this.percentComplete.keys[this.percentComplete.length - 1].time;
                for (float elapsedTime = 0; elapsedTime < runTime; elapsedTime += Time.unscaledDeltaTime)
                {
                    float percentComplete = this.percentComplete.Evaluate(elapsedTime);
                    effect.Tick(percentComplete);

                    yield return null;
                }

                effect.Tick(this.percentComplete.Evaluate(1));
            }
            else
            {
                yield return null;
            }
        }

        public IEnumerator TransitionIn()
        {
            if (this.TryGetComponent(out EffectDefinitionBase effect))
            {
                float runTime = this.percentComplete.keys[this.percentComplete.length - 1].time;
                for (float elapsedTime = runTime; elapsedTime >= 0; elapsedTime -= Time.unscaledDeltaTime)
                {
                    float percentComplete = this.percentComplete.Evaluate(elapsedTime);
                    effect.Tick(percentComplete);

                    yield return null;
                }

                effect.Tick(this.percentComplete.Evaluate(0));

                yield return null;
                effect.Finish();
            }
            else
            {
                yield return null;
            }
        }
    }
}