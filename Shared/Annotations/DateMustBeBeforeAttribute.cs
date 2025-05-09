using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Shared.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class DateMustBeBeforeAttribute : ValidationAttribute
    {
        public DateMustBeBeforeAttribute(string targetPropertyName)
        => TargetPropertyName = targetPropertyName;

        public string TargetPropertyName { get; }

        public string GetErrorMessage(string propertyName) =>
        $"'{propertyName}' muss vor '{TargetPropertyName}' liegen.";

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var targetValue = validationContext.ObjectInstance
            .GetType()
            .GetProperty(TargetPropertyName)
            ?.GetValue(validationContext.ObjectInstance, null);

            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue < DateTime.Now)
                {
                    return new ValidationResult($"{dateTimeValue} liegt in der Vergangenheit!");
                }
            }

            if ((DateTime?)value > (DateTime?)targetValue)
            {
                var propertyName = validationContext.MemberName ?? string.Empty;
                return new ValidationResult(GetErrorMessage(propertyName), new[] { propertyName });
            }

            return ValidationResult.Success;
        }
    }
}
