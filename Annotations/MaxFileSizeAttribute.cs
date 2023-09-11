using Microsoft.AspNetCore.Http;
using MKExpress.API.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;
            if (!(value is IFormFile)) return false;

            return ((IFormFile)value).Length <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return StaticValues.ErrorFileSize(_maxFileSize);
        }
    }
}