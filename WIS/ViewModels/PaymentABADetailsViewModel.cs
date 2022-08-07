using System;
using WIS.Models;

namespace WIS.ViewModels
{
    public class PaymentABADetailsViewModel
    {
        public PaymentABADetailsViewModel(ABATransaction transaction)
        {
                        
            TransactionId = transaction.transaction_id;
            TransactionDate = transaction.transaction_date;
            Apv = transaction.apv;
            Status = transaction.status;
            PaymentType = transaction.payment_type;
            OriginalAmount = transaction.original_amount;
            RefundAmount = transaction.refund_amount;
            FirstName = transaction.first_name;
            LastName = transaction.last_name;
            Email = transaction.email;
            Phone = transaction.phone;
            BankRef = transaction.bank_ref;            
        }

#region Property
        public string TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public string Apv { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public string OriginalAmount { get; set; }
        public string RefundAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BankRef { get; set; }
#endregion
    }



}

