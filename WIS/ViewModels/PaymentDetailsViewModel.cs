using System;
using WIS.Models;
using WIS.Services;
using Xamarin.Forms;

namespace WIS.ViewModels
{
    public class PaymentDetailsViewModel : BaseViewModel
    {
        string invoiceid;
        //0 = to pay 
        //2 = to validate ABA(list payment detail from ABA)
        //3 = to validate ACLEDA(list payment detail from ACLEDA)
        //1 = Paid(check note, load ABA or ACLEDA)
        public Command ValidateCommand { get; set; }

        
        public string Status { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string TotalAmount { get; set; }
        public string Apv { get; set; }
        public string Datetime { get; set; }
        public string OriginalCurrency { get; set; }
        public string TranId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string BankRef { get; set; }
        public string PayerAccount { get; set; }
        public string Phone { get; set; }
        public string PaymentType { get; set; }
        
        public PaymentDetailsViewModel(string id)
        {
            invoiceid = id;
            DataService.Instance.GetInvoiceDetails(id, (invoice) =>
             { 
                 if (invoice.is_paid == "2") // Always
                 {
                     DataService.Instance.ABATransactionCheck((abastatus) =>
                     {
                         Status = abastatus.status;
                         Description = abastatus.description;
                         Amount = abastatus.amount;
                         TotalAmount = abastatus.totalAmount;
                         Apv = abastatus.apv;
                         Datetime = abastatus.datetime;
                         OriginalCurrency = abastatus.original_currency;
                         TranId = abastatus.tran_id;
                         Firstname = abastatus.firstname;
                         Lastname = abastatus.lastname;
                         Email = abastatus.email;
                         BankRef = abastatus.bank_ref;
                         PayerAccount = abastatus.payer_account;
                         Phone = abastatus.phone;
                         PaymentType = abastatus.payment_type;

                     }, invoice);
                 }
             });
            this.ValidateCommand = new Command(validateClicked);

        }

        public void validateClicked(object obj)
        {
            DataService.Instance.InvoiceValidatePayment((result) =>
            {
            }, invoiceid);
        }
        

    }
}

