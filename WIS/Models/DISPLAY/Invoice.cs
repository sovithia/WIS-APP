using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using SQLite;

namespace WIS.Models
{
    
   
    public class InvoicePromotion
    {
        public InvoicePromotion(){}
        public string amount { get; set; }
        public string note { get; set; }
        public string detail { get; set; }
    }


    public class ABAInvoiceElement
    {
        public string name { get; set; }
        public float quantity { get; set; }
        public float price { get; set; }
    }

    public class InvoiceElement
    {

        
        public float amount { get; set; } 
        public string productname { get; set; } 
        public float quantity { get; set; } // ??
        public float price { get; set; } // ??
                                                
                                                        
        //public string promotion { get; set; } // invoice_promotion amount         
    }

    public class Invoice
    {
        public string id { get; set; }
        public string doc { get; set; } // Invoice  doc 
        public DateTime date { get; set; } // Invoice date 
        public DateTime date_due { get; set; } // Invoice date_due
        
        public string studentdisplayname {get;set;}
        //public List<InvoicePromotion> invoicepromotionList { get; set; }
        public List<InvoiceElement> invoicefeeList { get; set; }

        
        public string ABAItems()
        {
            List<ABAInvoiceElement> items = new List<ABAInvoiceElement>();
            foreach(InvoiceElement elem in invoicefeeList)
            {
                items.Add(new ABAInvoiceElement()
                {
                    name = elem.productname,
                    quantity = elem.quantity,
                    price = elem.price
                });
            }
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, items);            
            return Convert.ToBase64String(mStream.ToArray());
        }
        
        [JsonIgnore, Ignore]
        public string ID
        {
            get
            {
                return doc;
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