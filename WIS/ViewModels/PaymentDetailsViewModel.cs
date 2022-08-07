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

        
        public string status { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string totalAmount { get; set; }
        public string apv { get; set; }
        public string datetime { get; set; }
        public string original_currency { get; set; }
        public string tran_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string bank_ref { get; set; }
        public string payer_account { get; set; }
        public string phone { get; set; }
        public string payment_type { get; set; }
        
        public PaymentDetailsViewModel(string id)
        {
            invoiceid = id;
            DataService.Instance.GetInvoiceDetails(id, (invoice) =>
             { 
                 if (invoice.is_paid == "2") // Always
                 {
                     DataService.Instance.ABATransactionCheck((abastatus) =>
                     {
                         status = abastatus.status;
                         description = abastatus.description;
                         amount = abastatus.amount;
                         totalAmount = abastatus.totalAmount;
                         apv = abastatus.apv;
                         datetime = abastatus.datetime;
                         original_currency = abastatus.original_currency;
                         tran_id = abastatus.tran_id;
                         firstname = abastatus.firstname;
                         lastname = abastatus.lastname;
                         email = abastatus.email;
                         bank_ref = abastatus.bank_ref;

                         payer_account = abastatus.payer_account;
                         phone = abastatus.phone;
                         payment_type = abastatus.payment_type;

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

