using System;
using System.ComponentModel.DataAnnotations;
using QuickDelivery.Enums;

namespace QuickDelivery.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class ExternalProductShouldOrder5DaysInAdvanceAttribute : ValidationAttribute
    {
        private readonly string _dependantPropertyName;

        public ExternalProductShouldOrder5DaysInAdvanceAttribute(string dependantPropertyName)
        {
            _dependantPropertyName = dependantPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var dependentProperty = validationContext.ObjectType.GetProperty(_dependantPropertyName);
                var dependentPropertyValueAsString = (string)dependentProperty.GetValue(validationContext.ObjectInstance, null);
                var dependentPropertyValue = (ProductType) Enum.Parse(typeof(ProductType), dependentPropertyValueAsString);
                var currentPropertyValue = (int)value;

                if (dependentPropertyValue != ProductType.External)
                {
                    return ValidationResult.Success;
                }

                if (currentPropertyValue == 5)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult($"Property value should be 5 as '{_dependantPropertyName}' is of type {dependentPropertyValue}");
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
        }
    }
}
