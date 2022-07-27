using System;
namespace WIS.Models
{

    public enum ABAPAYMENTSTATE
    {
        ALL,
        APPROVED,
        DECLINED,
        PENDING,
        CANCELLED
    }

    // Search Engine Response
    public class ABATransaction
    {
        public ABATransaction(){}
        public string transaction_id { get; set; }
        public string transaction_date { get; set; }
        public string apv { get; set; }
        public string status { get; set; }
        public string payment_type { get; set; }
        public string original_amount { get; set; }
        public string refund_amount { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string bank_ref { get; set; }
    }
}

