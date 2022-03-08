using System;
namespace WIS.Validators.Rules
{
    public class isValidDateRule<T> : IValidationRule<T>
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

            DateTime fromDateValue;
            var formats = new[] { "dd MMM yyyy" };
            if (DateTime.TryParseExact(value.ToString(), formats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out fromDateValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
