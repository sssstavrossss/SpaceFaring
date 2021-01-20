using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Custom_Validations
{
    public static class ObjectValidator
    {
        public static bool PassValidation(object myObject)
        {
            if (myObject == null)
                return false;
            var validationContext = new ValidationContext(myObject);
            bool validation = Validator.TryValidateObject(myObject, validationContext, new List<ValidationResult>(), true);
            return validation;
        }
    }
}