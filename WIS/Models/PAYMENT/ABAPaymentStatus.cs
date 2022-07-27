using System;
namespace WIS.Models
{
    // Check Transaction response
    public class ABAPaymentStatus
    {
        public ABAPaymentStatus(){}
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
    }
}