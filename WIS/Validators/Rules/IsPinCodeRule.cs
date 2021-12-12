using System;
namespace WIS.Validators.Rules
{
    public class IsPinCodeRule<T> : IValidationRule<T>
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
            int outint;

            if (value == null)
                return false;

            if (value.ToString().Length != 4)
                return false;
            
            if (int.TryParse(value.ToString(), out outint))            
                return true;            
            else            
                return false;                    
        }
        #endregion
    }


}
