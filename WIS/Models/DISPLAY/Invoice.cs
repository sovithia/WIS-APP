using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace WIS.Models.DISPLAY
{
    public class Product
    {
        public string description { get; set; }
    }
   
    public class InvoicePromotion
    {
        public InvoicePromotion(){}

        string amount { get; set; }
    }

    public class InvoiceElement
    {
        public string amount { get; set; } // invoice_fees amount
        public Product product { get; set; }
         // invoice_fees -> product -> name 
        public string promotion { get; set; } // invoice_promotion amount         
    }

    public class Invoice
    {

        public string doc { get; set; } // Invoice  doc 
        public DateTime date { get; set; } // Invoice date 
        public DateTime date_due { get; set; } // Invoice date_due

        public StudentProfile studentprofile  { get; set;}         

        public List<InvoiceElement> invoicefeeList { get; set; }
        

        [JsonIgnore, Ignore]
        public string student
        {
            get
            {
                return studentprofile.firstname + " " + studentprofile.lastname;
            }
        }

        [JsonIgnore, Ignore]
        public string dID
        {
            get
            {
                return "Invoice No: " + doc;
            }

        }
    }
}


/*
public string invoice_number { get; set; }
public string invoice_date { get; set; }
public string firstname { get; set; }
public string lastname { get; set; }
public string tuition_price { get; set; }
public string tuition_description { get; set; }
public List<INVOICEDATA> datalist { get; set; }

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
(
*/