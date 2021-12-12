using System;
namespace WIS.Models
{
    public class SIGNUPREQUEST
    {
        public string type { get; set; }
        public string phone { get; set; }
        public string identifier { get; set; }
        public string birthdate { get; set; }
        public string password { get; set; }
    }
}
