using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SpottedZebra.UnityFoundation.Polish
{
    public sealed class MaterialEffect : EffectDefinitionBase
    {
        [Required]
        [Tooltip("The material to animate. `_PercentComplete` and `_Color` properties are expected.")]
        [SerializeField]
        private Material material;

        [Tooltip("The RenderFeature associated with this material. Toggled on/off as needed.")] [Required] [SerializeField]
        private ScriptableRendererFeature feature;

        [Tooltip("Color applied to the material's `_Color` property")] [SerializeField]
        private Color color = Color.black;

        private static readonly int MaterialPercentComplete = Shader.PropertyToID("_PercentComplete");
        
        private static readonly int MaterialColor = Shader.PropertyToID("_Color");

        protected override void OnPlay()
        {
            this.feature.SetActive(true);
            this.material.SetColor(MaterialEffect.MaterialColor, this.color);
        }

        protected override void OnTick(float percentComplete)
        {
            this.material.SetFloat(MaterialEffect.MaterialPercentComplete, percentComplete);
        }

        protected override void OnFinish()
        {
            this.feature.SetActive(false);
        }
    }
}