using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Misc
{
    public class ValidUsernameAttribute : ValidationAttribute
    {
        public ValidUsernameAttribute()
        {
            ErrorMessage = "The username should start with a letter and is only allowed to contain letters, digits and underscores(_).";
        }

        public override bool IsValid(object value)
        {
            if (value is string str)
            {
                if (str.Length < 5 || str.Length > 50)
                    return false; // too short or too long

                if (!char.IsLetter(str[0]))
                    return false;

                foreach (char ch in str)
                {
                    if (!char.IsLetterOrDigit(ch) && ch != '_')
                        return false;
                }

                return true;
            }

            return false;
        }
    }
}