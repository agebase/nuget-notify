﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NugetNotify.Core.Attributes
{
    public class AtLeastOneRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string[] _properties;

        public AtLeastOneRequiredAttribute(params string[] properties)
        {
            _properties = properties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_properties == null || _properties.Length < 1)
                return null;

            foreach (var property in _properties)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(property);
                if (propertyInfo == null)
                    return new ValidationResult($"unknown property {property}");

                var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
                if (!string.IsNullOrEmpty(propertyValue as string))
                    return null;

                if (propertyValue != null)
                    return null;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = "atleastonerequired"
            };
            rule.ValidationParameters["properties"] = string.Join(",", _properties);

            yield return rule;
        }
    }
}