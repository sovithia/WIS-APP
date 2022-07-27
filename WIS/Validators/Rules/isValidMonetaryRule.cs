using System;
using System.Text.RegularExpressions;

namespace WIS.Validators.Rules
{  
    public class isValidMonetaryRule<T> : IValidationRule<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Validation Message.
        /// </summary>
        public string ValidationMessage { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// Check the email has valid or not
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>returns bool value</returns>
        public bool Check(T value)
        {            
            if (value == null)
                return false;            

            value = (T)(object)value.ToString().Replace(" ", string.Empty);
            if (Regex.IsMatch(value.ToString(), @"^-?[0-9][0-9,\.]+$"))            
                return true;            
            else                                            
                return false;            
        }
        #endregion
    }
}

