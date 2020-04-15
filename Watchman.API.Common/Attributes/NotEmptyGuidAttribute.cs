using System;
using System.ComponentModel.DataAnnotations;

namespace Watchman.API.Common.Attributes
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = (Guid)value;
            bool isValid = !inputValue.Equals(Guid.Empty);
            return isValid;
        }
    }
}
