using Sirenix.OdinInspector.Editor.Validation;

[assembly:RegisterValidator(typeof(SpottedZebra.UnityFoundation.Navigation.Design.SceneRequiredValidator))]
namespace SpottedZebra.UnityFoundation.Navigation.Design
{
    public class SceneRequiredValidator : AttributeValidator<SceneRequiredAttribute, SceneReference>
    {
        protected override void Validate(ValidationResult result)
        {
            if (this.ValueEntry.SmartValue == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.ValueEntry.SmartValue.ScenePath))
            {
                result.ResultType = ValidationResultType.Error;
                result.Message = "Scene is required";
            }
        }
    }
}