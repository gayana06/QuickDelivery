using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QuickDelivery.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;

        public EnsureMinimumElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;

            if (list == null)
            {
                return false;
            }

            if (list.Count < _minElements)
            {
                return false;
            }

            return list.Cast<object>().All(listItem => listItem != null);
        }
    }
}
