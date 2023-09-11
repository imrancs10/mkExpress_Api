using Microsoft.AspNetCore.Http;
using MKExpress.API.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MKExpress.API.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly List<string> _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions?.ToList();
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;
            if (!(value is IFormFile)) return false;

            IFormFile file = (IFormFile)value;
            string fileExtension = Path.GetExtension(file.FileName);
            return _extensions.Contains(file.ContentType) &&
                   _extensions.Contains(fileExtension.ToLower());
        }

        public override string FormatErrorMessage(string name)
        {
            return StaticValues.ErrorInvalidContentType(_extensions.ToArray());
        }
    }
}