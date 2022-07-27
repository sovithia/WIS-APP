using System;
namespace WIS.Models
{    
    public class ABASTATUS
    {
        public string code { get; set; }
        public string message { get; set; }
        public string lang { get; set; }

    }
    // Payment Request Creation
    public class ABAPaymentRequestResponse
    {
        public ABASTATUS status { get; set; }
        public string description { get; set; }
        public string qrString { get; set; }
        public string abapay_deeplink { get; set; }
        public string app_store { get; set; }
        public string play_store { get; set; }
    }
}

