using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace WIS.Models
{
    public class INVOICELINE
    {
        public string description { get; set; }
        public string amount { get; set; }

        public string dfee
        {
            get
            {                
                return $"$ {amount}";
            }
        }
    }
    

    public class INVOICE
    {
        public string id { get; set; }
        public string invoice_number { get; set; }
        public string invoice_date { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string tuition_price { get; set; }
        public string tuition_description { get; set; }



        public List<INVOICELINE> lines { get; set; }
        public string proof { get; set; } // B64
        public string sigparent { get; set; } // B64
        public string sigregistrar { get; set; } // B64

        [JsonIgnore, Ignore]
        public string student
        {
            get
            {
                return firstname + " " + lastname;
            }
        }

        [JsonIgnore, Ignore]
        public string dID
        {
            get
            {
                return "Invoice No: " + invoice_number;
            }
            
        }

    }
}

