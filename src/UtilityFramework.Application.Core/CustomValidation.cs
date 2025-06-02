using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UtilityFramework.Application.Core
{
    public class EpochDateValidatorAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;
        private readonly DateTime _maxDate;

        public EpochDateValidatorAttribute(long minEpoch = long.MinValue, long maxEpoch = long.MaxValue)
        {
            _minDate = minEpoch == long.MinValue ? DateTime.MinValue : DateTimeOffset.FromUnixTimeSeconds(minEpoch).UtcDateTime;
            _maxDate = maxEpoch == long.MaxValue ? DateTime.MaxValue : DateTimeOffset.FromUnixTimeSeconds(maxEpoch).UtcDateTime;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is not long epoch)
            {
                return new ValidationResult("O valor deve ser um número inteiro representando o Epoch em segundos.");
            }

            var date = DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime;

            if (date < _minDate || date > _maxDate)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }

    public class MinValueAttribute(object minValue) : ValidationAttribute
    {
        private readonly object _minValue = minValue;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || value is not IComparable)
            {
                return ValidationResult.Success;
            }

            IComparable comparable = (IComparable)value;
            if (comparable.CompareTo(_minValue) >= 0)
            {
                return ValidationResult.Success;
            }

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = $"The value of {validationContext.DisplayName} must be greater than or equal to {_minValue}.";
            }

            return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, _minValue));
        }
    }

    public class MaxValueAttribute(object maxValue) : ValidationAttribute
    {
        private readonly object _maxValue = maxValue;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || value is not IComparable)
            {
                return ValidationResult.Success;
            }

            var comparableValue = (IComparable)value;

            if (comparableValue.CompareTo(_maxValue) <= 0)
            {
                return ValidationResult.Success;
            }

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = $"The value of {validationContext.DisplayName} must be less than or equal to {_maxValue}.";
            }

            return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, _maxValue));
        }
    }

    public class IsValidCpf : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return null;

            return Utilities.ValidCpf(value?.ToString()) == false
                ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName))
                : null;
        }
    }

    public class IsValidCnpj : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return null;

            return Utilities.ValidCnpj(value?.ToString()) == false
                ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName))
                : null;
        }
    }

    public class LimitElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;
        private readonly int? _maxElements;
        private readonly string _defaultErrorMessage;

        public LimitElementsAttribute(int minElements = 1, int maxElements = int.MaxValue, string defaultErrorMessage = "O campo {0} está inválido")
        {
            _minElements = minElements;
            _maxElements = maxElements;
            _defaultErrorMessage = defaultErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IList list == false)
                return null;

            var isValid = list.Count >= _minElements;

            if (isValid && _maxElements != null)
                isValid = list.Count >= _minElements && list.Count <= _maxElements;

            return isValid == false
                   ? new ValidationResult(string.Format(ErrorMessage ?? _defaultErrorMessage, validationContext.DisplayName, _minElements, _maxElements))
                   : null;
        }

    }
    public class RequiredIfAttribute : ValidationAttribute
    {
        RequiredAttribute _innerAttribute = new();
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }
        public bool IsEquals { get; set; }
        public bool IgnoreCase { get; set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue, bool isEquals = true, bool ignoreCase = false)
        {
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
            IsEquals = isEquals;
            IgnoreCase = ignoreCase;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isValid = true;
            try
            {
                object dependentValue = Utilities.GetValueByProperty(validationContext.ObjectInstance, DependentProperty);

                if (dependentValue != null)
                {
                    var hasValue = false;
                    var isString = TargetValue is string;
                    var isArray = Utilities.CheckIsCollection(TargetValue);
                    var isDependentArray = Utilities.CheckIsCollection(dependentValue);

                    if (isArray || isDependentArray)
                    {
                        var arr = Utilities.ToAnonymousArray(TargetValue);

                        for (int i = 0; i < arr.Length; i++)
                        {
                            var itemValue = arr.GetValue(i);

                            var dependentArr = Utilities.ToAnonymousArray(dependentValue);

                            for (int a = 0; a < dependentArr.Length; a++)
                            {
                                var dependentValueItem = dependentArr.GetValue(a);
                                var dependentItemType = dependentValueItem.GetType();
                                var isPrimitive = Utilities.IsPrimitive(dependentItemType);

                                var dependentIsEquals = isPrimitive
                                ? Utilities.Equals(itemValue, dependentValueItem, IgnoreCase)
                                : Utilities.Equals(itemValue, Utilities.GetValueByProperty(dependentValueItem, DependentProperty.Split('.').LastOrDefault()), IgnoreCase);

                                hasValue = dependentIsEquals == IsEquals;

                                if (hasValue)
                                    goto ENDLOOP;
                            }
                        }
                    }

                ENDLOOP:

                    if (
                        dependentValue.Equals(TargetValue) == IsEquals ||
                        isString && Utilities.EqualsIgnoreCase(dependentValue?.ToString(), TargetValue?.ToString()) == IsEquals ||
                        (isArray || isDependentArray) && hasValue)
                    {
                        isValid = _innerAttribute.IsValid(value);
                    }
                }
                else if (TargetValue == null && IsEquals)
                {
                    isValid = _innerAttribute.IsValid(value);
                }

                string specificErrorMessage = string.Format(
                    string.IsNullOrEmpty(ErrorMessage) == false
                    ? ErrorMessage
                    : "{0} is required",
                    validationContext.DisplayName);

                return isValid
                ? ValidationResult.Success
                : new ValidationResult(specificErrorMessage,
                    new[] { validationContext.MemberName });

            }
            catch (Exception)
            {
                throw;
            }

        }
    }

}
