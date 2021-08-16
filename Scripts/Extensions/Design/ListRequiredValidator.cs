using System.Collections;
using Sirenix.OdinInspector.Editor.Validation;
using SpottedZebra.UnityFoundation.Extensions.Design;

[assembly:RegisterValidator(typeof(ListRequiredValidator))]
namespace SpottedZebra.UnityFoundation.Extensions.Design
{
    public class ListRequiredValidator : AttributeValidator<ListRequiredAttribute>
    {
        protected override void Validate(ValidationResult result)
        {
            bool hasError;
            if (this.Property.ValueEntry.WeakSmartValue == null)
            {
                // probably not a list
                hasError = false;
            }
            else
            {
                ICollection collection = this.Property.ValueEntry.WeakSmartValue as ICollection;
                if (collection == null)
                {
                    // definitely not a list
                    hasError = false;
                }
                else
                {
                    hasError = collection.Count == 0;
                }
            }

            if (hasError)
            {
                result.ResultType = ValidationResultType.Error;
                result.Message = this.Property.NiceName + " is empty";
            }
        }
    }
}