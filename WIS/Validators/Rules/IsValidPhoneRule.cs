using System;
using System.Text.RegularExpressions;

namespace WIS.Validators.Rules
{
    public class IsValidPhoneRule<T> : IValidationRule<T>
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

            //DateTime fromDateValue;
            //var formats = new[] { "dd/MM/yy" };
            //if (DateTime.TryParseExact(value.ToString(), formats,
            //        System.Globalization.CultureInfo.InvariantCulture,
            //        System.Globalization.DateTimeStyles.None, out fromDateValue))
            if (value == null)
                return false;
            value = (T)(object) value.ToString().Replace(" ", string.Empty);            
            if (Regex.IsMatch(value.ToString(), @"^\d+$"))                
            {
                return true;
            }
            else
            {
                string onlynumber = value.ToString().Substring(1);
                if (Regex.IsMatch(onlynumber, @"^\d+$"))
                    return true;
                else
                    return false;                
            }
        }
        #endregion
    }

    
}
