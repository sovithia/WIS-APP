using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WIS.Models;
using WIS.Services;
using WIS.Validators;
using WIS.Validators.Rules;
using WIS.Views;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class PaymentListSearchViewModel : BaseViewModel
    {
        public PaymentListSearchViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.SearchCommand = new Command(this.SearchClicked);
            this.ItemSelectedCommand = new Command(ItemSelected);
        }

        #region Command

       
        public Command SearchCommand { get; set; }
        public Command ItemSelectedCommand { get; set; }
        #endregion

        #region Fields

        private ValidatableObject<string> date_from;
        private ValidatableObject<string> date_to;
        private ValidatableObject<string> from_amount;
        private ValidatableObject<string> to_amount;
        private ValidatableObject<string> status;
        private bool isLoading;
        #endregion



        #region Property
        public ObservableCollection<ABATransaction> Transactions { get; set; }


        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.SetProperty(ref this.isLoading, value);
            }
        }

        public ValidatableObject<string> DateFrom
        {
            get{
                return this.date_from;
            }
            set
            {
                if (this.date_from == value){
                    return;
                }
                this.SetProperty(ref this.date_from, value);
            }
        }

        public ValidatableObject<string> DateTo
        {
            get
            {
                return this.date_to;
            }
            set
            {
                if (this.date_to == value)
                {
                    return;
                }
                this.SetProperty(ref this.date_to, value);
            }
        }

        public ValidatableObject<string> FromAmount
        {
            get
            {
                return this.from_amount;
            }
            set
            {
                if (this.from_amount == value)
                {
                    return;
                }
                this.SetProperty(ref this.from_amount, value);
            }
        }

        public ValidatableObject<string> ToAmount
        {
            get
            {
                return this.to_amount;
            }
            set
            {
                if (this.to_amount == value)
                {
                    return;
                }
                this.SetProperty(ref this.to_amount, value);
            }
        }

        public ValidatableObject<string> Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (this.status == value)
                {
                    return;
                }
                this.SetProperty(ref this.status, value);
            }
        }
        #endregion 

        public bool AreFieldsValid()
        {
            bool isDateFromValid = this.DateFrom.Validate();
            bool isDateToValid = this.DateTo.Validate();
            bool isFromAmountValid = this.FromAmount.Validate();
            bool isToAmount = this.ToAmount.Validate();
            return isDateFromValid && isDateToValid && isFromAmountValid && isToAmount;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.IsLoading = false;
            this.DateFrom = new ValidatableObject<string>() { Value = "27 Jul 2022" };
            this.DateTo = new ValidatableObject<string>() { Value = "27 Jul 2022" };
            this.FromAmount = new ValidatableObject<string>() { Value = "10"}; 
            this.ToAmount = new ValidatableObject<string>() { Value = "5000" }; 
            this.Status = new ValidatableObject<string>() { Value = "ALL" };
         }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            this.DateFrom.Validations.Add(new isValidDateRule<string> { ValidationMessage = "Invalid date" });
            this.DateTo.Validations.Add(new isValidDateRule<string> { ValidationMessage = "Invalid date" });
            this.FromAmount.Validations.Add(new isValidMonetaryRule<string> { ValidationMessage = "Invalid amount" });
            this.ToAmount.Validations.Add(new isValidMonetaryRule<string> { ValidationMessage = "Invalid amount" });           
            this.Status.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Invalid status" });
        }

        private string formatDate(string date)
        {
            DateTime fromDateValue;
            var formats = new[] { "dd MMM yyyy" };
            
            if (DateTime.TryParseExact(date.ToString(), formats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out fromDateValue))
                return fromDateValue.ToString("yyyy-MM-d") + " 00:00:00";
            else
                return null;
        }

        private void SearchClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                IsLoading = true;
                Transactions = new ObservableCollection<ABATransaction>();
                DataService.Instance.ABAPaymentSearch((list) => {
                    IsLoading = false;
                    foreach(ABATransaction transaction in list){
                        Transactions.Add(transaction);
                    }
                }, formatDate(DateFrom.Value), formatDate(DateTo.Value),FromAmount.Value,ToAmount.Value,Status.Value);               
            }            
        }


        private void ItemSelected(object selectedItem)
        {
            ABATransaction transaction = (ABATransaction)((Syncfusion.ListView.XForms.ItemTappedEventArgs)selectedItem).ItemData;
            PaymentDetailsPage page = new PaymentDetailsPage(transaction);
            Shell.Current.Navigation.PushAsync(page);
            //Shell.Current.GoToAsync($"InvoiceDetails?ID={transaction.id}&IsSubmitable=true");
        }



    }
}

