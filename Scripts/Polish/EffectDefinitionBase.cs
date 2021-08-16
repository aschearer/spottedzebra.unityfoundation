using UnityEngine;

namespace SpottedZebra.UnityFoundation.Polish
{
    public abstract class EffectDefinitionBase : MonoBehaviour
    {
        public void Play()
        {
            this.OnPlay();
        }

        public void Tick(float percentComplete)
        {
            this.OnTick(percentComplete);
        }

        public void Finish()
        {
            this.OnFinish();
        }
        
        protected abstract void OnPlay();

        protected abstract void OnTick(float percentComplete);

        protected abstract void OnFinish();
    }
}