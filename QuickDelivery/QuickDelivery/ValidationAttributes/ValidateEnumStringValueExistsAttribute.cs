using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class ValidateEnumStringValueExistsAttribute : DataTypeAttribute
    {
        private readonly Type _enumType;

        public ValidateEnumStringValueExistsAttribute(Type enumType) : base("Enumeration")
        {
            _enumType = enumType;
        }

        public override bool IsValid(object value)
        {
            if (_enumType == null)
            {
                throw new InvalidOperationException("Type cannot be null");
            }

            if (!_enumType.IsEnum)
            {
                throw new InvalidOperationException("Type must be an enum");
            }

            if (value == null)
            {
                return false;
            }

            if (value is IEnumerable && !(value is string))
            {
                return IsEnumerableValid((IEnumerable)value);
            }

            return IsObjectValid(value);
        }

        private bool IsObjectValid(object value)
        {
            if (!(value is string))
            {
                return false;
            }

            return Enum.IsDefined(_enumType, value);
        }

        private bool IsEnumerableValid(IEnumerable list)
        {
            foreach (var item in list)
            {
                if (!IsObjectValid(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
