using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Space101.Custom_Validations
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParse(value.ToString(), out dateTime);

            return (isValid && dateTime.Date >= DateTime.Now.Date);
        }
    }
}